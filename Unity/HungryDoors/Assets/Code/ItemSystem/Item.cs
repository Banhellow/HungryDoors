using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item: MonoBehaviour, IUsable
{
    private GameObject itemVFX;
    private bool _isInUsage = false;
    public bool isInUsage { get => _isInUsage; set => _isInUsage = value; }

    private void Start()
    {
        Use();
    }
    public virtual void Use()
    {
        Debug.Log("Object has used");
    }
}
