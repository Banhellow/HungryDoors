using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public ParticleSystem useParticle;
    public Item projectilePrefab;


    private void Start()
    {
        data.type = ItemType.Weapon;
        useParticle.Stop();
    }

    public override Item Use()
    {
        if(data.weaponType == WeaponType.melee)
        {
            UseMelee();
            return ChangeItemDurability(1);
        }
        else
        {
            UseShoot();
            return ChangeItemDurability(1);
        }
    }

    void UseMelee()
    {
        useParticle.Play();
    }

    void UseShoot()
    {
        var projectile = itemManager.InstantiateItem(projectilePrefab, transform.position, Quaternion.identity);
        var projectileRB = projectile.GetComponent<Rigidbody>();
        soundManager.PlaySfx(SFX.Throw);
        projectileRB.isKinematic = false;
        projectile.isInUsage = true;
        projectile._hasLanded = false;
        Vector3 direction = GetComponentInParent<PlayerController>().LookDirection;
        projectileRB.AddForce(direction * data.pushForce);
    }
}
