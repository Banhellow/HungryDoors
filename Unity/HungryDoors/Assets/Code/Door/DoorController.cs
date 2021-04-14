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
        Debug.Log(phrase);
    }


    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == Tags.PICKUP_ITEM)
        {
            var item = collision.GetComponentInParent<Item>();
            string phrase;
            switch (item.data.type)
            {
                case ItemType.Food:
                    bool isCorrect = item.data.foodType == preferedFoodType ? true : false;
                    phrase = conversation.GetPhraseByFoodType(item.data.foodType,
                        isCorrect);
                    guiManager.ShowDialogBox(null, phrase, 2f);
                    if (isCorrect) OpenDoor();
                    else SpawnEnemies();
                    break;
                case ItemType.Weapon:
                    phrase = conversation.GetPhraseByWeaponType(item.data.weaponType);
                    guiManager.ShowDialogBox(null, phrase, 2f);
                    SpawnEnemies();
                    break;
            }
        }
    }

    public void OpenDoor()
    {
        gameObject.SetActive(false);
    }

    public void SpawnEnemies()
    {
        Debug.Log("Fail! Spawn more enemies");
    }
}


