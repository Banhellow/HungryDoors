using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public FoodType preferedFoodType;
    public Conversation conversation;
    void Start()
    {
        conversation = new Conversation();
        string phrase = conversation.GetPhraseByFoodType(FoodType.wooden, false);
        Debug.Log(phrase);
    }


    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == Tags.PICKUP_ITEM)
        {
            var itemData = GetComponent<ItemData>();
            string phrase;
            switch (itemData.type)
            {
                case ItemType.Food:
                    phrase = conversation.GetPhraseByFoodType(itemData.foodType,
                        itemData.foodType == preferedFoodType ? true : false);
                    Debug.Log(phrase);
                    break;
                case ItemType.Weapon:
                    phrase = conversation.GetPhraseByWeaponType(itemData.weaponType);
                    Debug.Log(phrase);
                    break;
            }
        }
    }

    public void OpenDoor()
    {

    }
}


