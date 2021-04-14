using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Food : Item
{
    private void Start()
    {
        data.type = ItemType.Food;
    }
    public override void ChangeItemDurability()
    {
        if (durability >= data.maxDurabilityHidden)
            Destroy(gameObject);
        else
            durability++;
    }
    public override void Use()
    {
        ChangeItemDurability();
        Debug.Log("Food is Used, food type: " + data.foodType);
        //base.Use();
    }
}
