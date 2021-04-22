using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    public FoodType targetType;
    public ItemData dropItem;
    public GameObject lastCollided;
    void Start()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag(Tags.PICKUP_ITEM) && collision.gameObject != lastCollided)
        {
            lastCollided = collision.gameObject;
            var item = collision.gameObject.GetComponent<Item>();
            if(item.data.foodType == targetType)
            {
                item.data.relatedItem = dropItem;
            }
        }
    }
}
