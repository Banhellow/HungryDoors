using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    [ShowAssetPreview(64,64)]
    public Sprite icon;
    public ItemType type;
    public FoodType foodType;
    public WeaponType weaponType;
    public AnimationType animationType;
    public int maxUsageCount = 1;
}
