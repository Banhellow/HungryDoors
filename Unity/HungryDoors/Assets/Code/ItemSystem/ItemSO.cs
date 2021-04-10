using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Item_", menuName ="Assets/Items")]
public class ItemSO : ScriptableObject
{
    public GameObject itemPrefab;
    public GameObject itemVFX;
    public ItemType itemType;

    public void CreateItem()
    {
        switch(itemType)
        {
            case ItemType.Food:
                
                break;
        }
    }
}
