using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class Character : MonoBehaviour
{
    public bool isPlayer = false;

    [Header("Animations")]
    public Animator animator;
    protected const string isMovingParam = "IsMoving";
    protected const string attackParam = "Attack";
    protected const string shootParam = "Shoot";
    protected const string pickupParam = "PickupItem";
    protected const string dieParam = "Die";
    protected const string healParam = "Heal";
    protected const string getDamageParam = "GetDamage";


    public virtual void OnHealthUpdated(int healthChange)
    {
        if (healthChange > 0)
            animator.SetTrigger(healParam);
        else
            animator.SetTrigger(getDamageParam);
    }

    public virtual void Die()
    {
        Debug.Log("Character Die");
        animator.SetTrigger(dieParam);
    }

}
