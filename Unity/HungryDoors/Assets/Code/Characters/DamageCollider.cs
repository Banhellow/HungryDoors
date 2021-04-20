using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [ReadOnly] public bool isInUse = false;

    public bool isPlayersItem = false;
    public int damage;

    internal void OnPickupByPlayer()
    {
        isInUse = true;
        isPlayersItem = true;
    }
}
