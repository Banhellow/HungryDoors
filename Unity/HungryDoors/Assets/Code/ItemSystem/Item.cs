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
    [ReadOnly] public int durability = 0;
    private bool _isInUsage = false;
    [Inject]
    private GUIManager gUIManager;
    public bool isInUsage { get => _isInUsage; set => _isInUsage = value; }

    private void Start()
    {

    }

    public virtual Item Use()
    {
        Debug.Log("Object has bee used");
        return ChangeItemDurability();
    }

    public virtual Item ChangeItemDurability()
    {
        durability++;
        if (durability >= data.maxUsageCount)
        {
            var Item = ShowRealItem();
            Destroy(gameObject);
            return Item;
        }
        return this;
    }

    internal Item ShowRealItem()
    {
        if (data.relatedItem == null)
            return null;
        var item = Instantiate(data.relatedItem.selfItem, transform.position, Quaternion.identity);
        item.OnPickup(transform.parent);
        gUIManager.UpdatePlayerItem(item);
        item.data = data.relatedItem;
        return item;
    }

    internal void OnPickup(Transform parentTR)
    {
        Debug.Log($"OnPickup {this.name}");
        transform.SetParent(parentTR);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;

        Destroy(pickupVisuals);
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<CapsuleCollider>());
        isInUsage = true;
    }
}
