using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DoorController : MonoBehaviour
{
    [Inject]
    private GUIManager guiManager;
    public FoodType preferedFoodType;
    public Conversation conversation;
    void Start()
    {
        conversation = new Conversation();
        string phrase = conversation.GetPhraseByFoodType(FoodType.wooden, false);
        guiManager.ShowDialogBox(null, phrase, 2);
        Debug.Log(phrase);
    }


    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == Tags.PICKUP_ITEM)
        {
            var itemData = collision.GetComponentInParent<ItemData>();
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


