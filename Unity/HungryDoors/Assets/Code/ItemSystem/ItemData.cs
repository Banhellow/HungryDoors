using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    [ShowAssetPreview(64,64)]
    public Sprite icon;
    public ItemType type;
    public FoodType foodType;
    public WeaponType weaponType;
    public AnimationType animationType;
    public int maxUsageCount = 1;
    public int maxDurabilityHidden = 1;
    public Item hiddenItemPrefab;
}
