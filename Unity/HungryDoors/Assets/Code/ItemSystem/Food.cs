using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Food : Item
{
    private void Start()
    {
        data.type = ItemType.Food;
    }
    public override Item ChangeItemDurability()
    {
        if (durability >= data.maxDurability)
            Destroy(gameObject);
        else
            durability++;
        return this;
    }
    public override Item Use()
    {
        ThrowItem(data.pushForce);
        return ChangeItemDurability();
        //base.Use();
    }
}
