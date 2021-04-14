using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NaughtyAttributes;
public class Item: MonoBehaviour, IUsable
{
    public ItemData data;
    public GameObject itemVFX;
    public GameObject pickupVisuals;
    [ReadOnly] public int durability = 0;
    private bool _isInUsage = false;
    public bool isInUsage { get => _isInUsage; set => _isInUsage = value; }

    private void Awake()
    {
       // data = GetComponent<ItemData>();
    }

    private void Start()
    {

    }

    public virtual void Use()
    {
        ChangeItemDurability();
        Debug.Log("Object has used");
    }

    public virtual void ChangeItemDurability()
    {
        durability++;
        if (durability >= data.maxUsageCount)
        {
            var Item = ShowRealItem();
            Item.Use();
            Destroy(gameObject);         
        }
    }

    internal Item ShowRealItem()
    {
        var item = Instantiate(data.hiddenItemPrefab, transform.position, Quaternion.identity);
        item.OnPickup(transform.parent);
        item.data = data;
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
