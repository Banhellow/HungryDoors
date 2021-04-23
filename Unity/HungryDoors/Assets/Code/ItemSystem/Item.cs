using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NaughtyAttributes;
using DG.Tweening;
using Zenject;
public class Item : MonoBehaviour, IUsable
{
    public ItemData data;
    public GameObject itemVFX;
    public GameObject pickupVisuals;
    public Rigidbody itemRB;
    public Collider itemCollider;
    [ReadOnly] public int durability = 0;

    private GUIManager GUIManager;
    protected ItemManager itemManager;
    protected SoundManager soundManager;
    protected DoorController doorController;
    private bool _isInUsage = false;
    public bool _hasLanded = true;
    public bool isOwnByPlayer = false;
    public bool _isUsed;

    public bool isInUsage { get => _isInUsage; set => _isInUsage = value; }

    [Inject]
    public void Init(GUIManager gui, ItemManager itemMan, SoundManager sound, DoorController controller)
    {
        GUIManager = gui;
        itemManager = itemMan;
        soundManager = sound;
        doorController = controller;
    }

    private void Awake()
    {
        itemCollider = GetComponent<Collider>();
    }

    private void Start()
    {
    }

    public virtual void OnCollisionEnter(Collision collision)
    {

        if (!collision.gameObject.CompareTag(Tags.PLAYER) &&
            !collision.gameObject.CompareTag(Tags.FLOOR) && !_hasLanded)
        {
            _hasLanded = true;
            Debug.Log("Collision detected: " + collision.gameObject);
            ChangeItemDurability(data.damage);
            isInUsage = false;
        }
        else if(collision.gameObject.CompareTag(Tags.PICKUP_ITEM) && _isUsed)
        {
            var item = collision.gameObject.GetComponent<Item>();
            if(!item._isUsed)
                item.ChangeItemDurability(data.damage);
        }
    }

    public virtual Item Use()
    {
        if (data.usageType == UsageType.Throw)
        {
            return ThrowItem(data.pushForce);
        }
        else
        {
            _isUsed = true;
            DOVirtual.DelayedCall(0.1f, ()=> _isUsed = false);
            return ChangeItemDurability(1);
        }

    }

    public virtual Item ThrowItem(float force)
    {
        soundManager.PlaySfx(SFX.Throw);
        itemRB.isKinematic = false;
        itemCollider.enabled = true;
        _hasLanded = false;
        Vector3 lookAtDir = GetComponentInParent<PlayerController>().LookDirection;
        transform.SetParent(null);
        itemRB.AddForce(lookAtDir * force);

        return null;
    }
    public virtual Item ChangeItemDurability(int changeValue)
    {
        durability += changeValue;
        if (isInUsage)
            GUIManager.UpdateItemDurability(this);

        soundManager.PlaySfx(data.sfx);

        if (durability >= data.maxDurability)
        {
            var Item = ShowRealItem();

            if (isInUsage)
                GUIManager.ItemLost();

            soundManager.PlaySfxWithDelay(SFX.ItemBreaks, 0.7f);
            Instantiate(data.destroyVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return Item;
        }
        return this;
    }

    internal Item ShowRealItem()
    {
        if (data.relatedItem == null)
            return null;
        var item = itemManager.InstantiateItem(data.relatedItem.selfItem, transform.position, Quaternion.identity);
        item.data = data.relatedItem;
        if (this.isInUsage)
        {
            item.OnPickup(transform.parent, true);
            GUIManager.UpdatePlayerItem(item);
            return item;
        }
        else
        {
            item.transform.position = transform.position;
            item.GetComponent<Rigidbody>().isKinematic = false;
            return null;
        }
    }

    internal void OnPickup(Transform parentTR, bool pickedUpByPlayer)
    {
        Debug.Log($"OnPickup {this.name}");
        isOwnByPlayer = pickedUpByPlayer;
        itemRB.isKinematic = true;
        itemCollider.enabled = false;
        transform.SetParent(parentTR);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
        Destroy(pickupVisuals);
        if (data.isQuestItem)
            doorController.GiveCheat(data.level, data.cheatType);
        isInUsage = true;
    }
}
