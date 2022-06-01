using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Food : Item
{
    private void Start()
    {
        data.type = ItemType.Food;
    }
    public override Item ChangeItemDurability(int value)
    {
        if (durability >= data.maxDurability)
            Destroy(gameObject);
        else
            durability+= value;
        return this;
    }
    public override Item Use()
    {
        ThrowItem(data.pushForce);
        return ChangeItemDurability(1);
        //base.Use();
    }
}
