using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using NaughtyAttributes;

public class DoorController : MonoBehaviour
{
    [Inject]
    private GUIManager guiManager;
    [ShowAssetPreview(64,64)]
    public Sprite doorImage;
    public FoodType preferedFoodType;
    public Conversation conversation;
    void Start()
    {
        conversation = new Conversation();
        string cheat = conversation.GetCheatByHintAndLevel(1,"A");
        Debug.Log(cheat);
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
                    guiManager.ShowDialogBox(doorImage, phrase, 2f);
                    if (isCorrect) OpenDoor();
                    else SpawnEnemies();
                    break;
                case ItemType.Weapon:
                    phrase = conversation.GetPhraseByWeaponType(item.data.weaponType);
                    guiManager.ShowDialogBox(doorImage, phrase, 2f);
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


