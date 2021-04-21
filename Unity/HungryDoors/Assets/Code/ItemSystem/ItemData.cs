using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemData
{
    [ShowAssetPreview(64,64)]
    public Sprite icon;

    [Header("Item types")]
    public ItemType type;
    public FoodType foodType;
    public WeaponType weaponType;
    public UsageType usageType;

    [Header("Animations")]
    public AnimationType animationType;
    [Header("SFX")]
    public SFX sfx;

    [Header("Item settings")]
    public int damage; 
    public int maxDurability = 1;
    public float pushForce = 1000f;
    public Item selfItem;
    public ItemData relatedItem;

    [Header("Quest settings")]
    public bool isQuestItem = false;
    [ShowIf("isQuestItem")]
    public CheatType cheatType;
    [ShowIf("isQuestItem")]
    public int level;
}
