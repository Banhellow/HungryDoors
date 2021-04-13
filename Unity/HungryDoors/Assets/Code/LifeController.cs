﻿using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class LifeController : MonoBehaviour
{
    [Header("LifeController")]
    public int startHealth = 100;
    [ReadOnly] public int currentHealth = 100;
    [ReadOnly] public Character myCharacter;

    public void Awake()
    {
        myCharacter = GetComponent<Character>();
        currentHealth = startHealth;
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;

        // dead
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            myCharacter.Die();
        }
    }

    public void Heal(int healthPoints)
    {
        currentHealth += healthPoints;

        if (currentHealth > startHealth)
            currentHealth = startHealth;
    }





    [Button]
    private void DEV_Get60Damage()
    {
        GetDamage(60);
    }
}