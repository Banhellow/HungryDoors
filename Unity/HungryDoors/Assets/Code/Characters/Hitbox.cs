using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private LifeController lifeController;

    private void Awake()
    {
        lifeController = GetComponentInParent<LifeController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Hitbox OnTriggerEnter : {other}");
        DamageCollider dc = other.gameObject.GetComponent<DamageCollider>();
        
        // dont hit yoursefl
        if (dc.isPlayersWeapon == lifeController.myCharacter.isPlayer)
            return;

        lifeController.GetDamage(dc.damage);
    }
}
