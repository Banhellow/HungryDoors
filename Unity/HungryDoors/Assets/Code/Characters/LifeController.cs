using DG.Tweening;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject.SpaceFighter;

public class LifeController : MonoBehaviour
{
    [Header("LifeController")]
    public int startHealth = 100;
    [ReadOnly] public int currentHealth = 100;
    [ReadOnly] public Character myCharacter;
    public Image lifeFillImage;

    public void Awake()
    {
        myCharacter = GetComponent<Character>();
        currentHealth = startHealth;
        UpdateLifeFillImage();
    }

    internal float GetCurrentHealth()
    {
        return (float)currentHealth / (float)startHealth;
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        UpdateLifeFillImage();
        myCharacter.OnHealthUpdated(-damage);

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
        UpdateLifeFillImage();
        myCharacter.OnHealthUpdated(healthPoints);

        if (currentHealth > startHealth)
            currentHealth = startHealth;
    }

    public void UpdateLifeFillImage()
    {
        if (lifeFillImage != null)
            lifeFillImage.DOFillAmount((float)currentHealth / (float)startHealth, 0.2f);
    }



    [Button]
    private void DEV_Get60Damage()
    {
        GetDamage(60);
    }
}
