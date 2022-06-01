using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

public class Character : MonoBehaviour
{
    [Inject]
    public SoundManager soundManager;

    public bool isPlayer = false;
    public bool isDead = false;

    [Header("Animations")]
    public Animator animator;
    protected const string isMovingParam = "IsMoving";
    protected const string isDeadParam = "IsDead";
    protected const string attackParam = "Attack";
    protected const string shootParam = "Shoot";
    protected const string pickupParam = "PickupItem";
    protected const string healParam = "Heal";
    protected const string getDamageParam = "GetDamage";


    public virtual void OnHealthUpdated(int healthChange)
    {
        if (isDead)
            return;

        if (healthChange > 0)
            animator.SetTrigger(healParam);
        else
        {
            animator.SetTrigger(getDamageParam);
            soundManager.PlaySfx(SFX.Damage);
        }
    }

    public virtual void Die()
    {
        if (isDead)
            return;

        Debug.Log("Character Die");
        isDead = true;
        animator.SetTrigger(isDeadParam);
        soundManager.PlaySfx(SFX.Death);
    }

}
