using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NaughtyAttributes;
using Zenject;
public class Item: MonoBehaviour, IUsable
{
    public ItemData data;
    public GameObject itemVFX;
    public GameObject pickupVisuals;
    public Rigidbody itemRB;
    [ReadOnly] public int durability = 0;

    private GUIManager GUIManager;
    private ItemManager itemManager;
    private bool _isInUsage = false;
    private bool _hasLanded = true;
    public bool isInUsage { get => _isInUsage; set => _isInUsage = value; }

    [Inject]
    public void Init(GUIManager gui, ItemManager itemMan)
    {
        GUIManager = gui;
        itemManager = itemMan;
    }
    private void Start()
    {
    }

    public virtual void OnCollisionEnter(Collision collision)
    {

        if(!collision.gameObject.CompareTag(Tags.PLAYER) &&
            !collision.gameObject.CompareTag(Tags.FLOOR) && !_hasLanded)
        {
            _hasLanded = true;
           Debug.Log("Collision detected: " + collision.gameObject);
           ChangeItemDurability();
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
            return ChangeItemDurability();
        }

    }

    public virtual Item ThrowItem(float force)
    {
        itemRB.isKinematic = false;
        _hasLanded = false;
        Vector3 lookAtDir = GetComponentInParent<PlayerController>().LookDirection;
        transform.SetParent(null);
        itemRB.AddForce(lookAtDir * force);
        isInUsage = false;
        return null;
    }
    public virtual Item ChangeItemDurability()
    {
        durability++;
        if(isInUsage)
            GUIManager.UpdateItemDurability(this);

        if (durability >= data.maxDurability)
        {
            var Item = ShowRealItem();

            if(isInUsage)
                GUIManager.ItemLost();

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
            item.OnPickup(transform.parent);
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

    internal void OnPickup(Transform parentTR)
    {
        Debug.Log($"OnPickup {this.name}");
        itemRB.isKinematic = true;
        transform.SetParent(parentTR);

        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
        Destroy(pickupVisuals);
        //Destroy(GetComponent<Rigidbody>());
        //Destroy(GetComponent<CapsuleCollider>());
        isInUsage = true;
    }
}
