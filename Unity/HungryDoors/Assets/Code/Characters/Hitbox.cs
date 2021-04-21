using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private LifeController lifeController;

    private float damageMinDelay = 0.5f;
    private float lastDamageTime = 0;

    private void Awake()
    {
        lifeController = GetComponentInParent<LifeController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Hitbox OnTriggerEnter : {other}");
        Item dc = other.gameObject.GetComponent<Item>();

        if (dc == null)
            return;

        // check if is in use
        if (dc.isInUsage == false)
            return;

        // dont hit yoursefl
        if (dc.isOwnByPlayer == lifeController.myCharacter.isPlayer)
            return;

        if (Time.realtimeSinceStartup >= lastDamageTime + damageMinDelay)
        {
            lastDamageTime = Time.realtimeSinceStartup;
            lifeController.GetDamage(dc.data.damage);
        }
    }
}
