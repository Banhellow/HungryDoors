using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NaughtyAttributes;
[RequireComponent(typeof(ItemData))]
public class Item: MonoBehaviour, IUsable
{
    public ItemData data;
    public GameObject itemVFX;
    public GameObject pickupVisuals;
    [ReadOnly] public int usageCount = 0;
    private bool _isInUsage = false;
    public bool isInUsage { get => _isInUsage; set => _isInUsage = value; }

    private void Awake()
    {
        data = GetComponent<ItemData>();
    }

    private void Start()
    {

    }

    public virtual void Use()
    {
        usageCount++;
        if(usageCount >= data.maxUsageCount)
        {
            Destroy(gameObject);
        }
        Debug.Log("Object has used");
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
