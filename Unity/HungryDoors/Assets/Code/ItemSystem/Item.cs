using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(ItemData))]
public class Item: MonoBehaviour, IUsable
{
    protected ItemData data;
    public GameObject itemVFX;
    private bool _isInUsage = false;
    public bool isInUsage { get => _isInUsage; set => _isInUsage = value; }

    private void Awake()
    {
        data = GetComponent<ItemData>();
    }

    private void Start()
    {

        Use();
    }
    public virtual void Use()
    {
        Debug.Log("Object has used");
    }
}
