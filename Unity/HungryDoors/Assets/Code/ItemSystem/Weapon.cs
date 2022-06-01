using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public ParticleSystem meleeParticle;
    public Item projectilePrefab;
    public float bombRadius;
    public float bombForce;

    private void Start()
    {
        if (meleeParticle != null)
            meleeParticle.Stop();
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag(Tags.FLOOR) && !collision.gameObject.CompareTag(Tags.PLAYER)
            && !_hasLanded)
        {
            if(data.weaponType == WeaponType.projectile)
            {
                var objects = Physics.OverlapSphere(transform.position, bombRadius);
                for (int i = 0; i < objects.Length; i++)
                {
                    var item = objects[i].GetComponent<Item>();
                    var enemies = objects[i].GetComponent<LifeController>();
                    //items
                    if (item != null)
                    {
                        item.itemRB.AddForce((item.transform.position - transform.position).normalized * bombForce);
                        item.ChangeItemDurability(data.damage);

                    }
                    //enemies 
                    if (enemies != null)
                    {
                        enemies.GetDamage(data.damage);
                    }
                }
                ChangeItemDurability(5);
            }
            else
            {
                base.OnCollisionEnter(collision);
            }

        }
    }

    public override Item Use()
    {
        if(data.weaponType == WeaponType.melee)
        {
            UseMelee();
            return ChangeItemDurability(1);
        }
        else if(data.weaponType == WeaponType.shoot)
        {
            UseShoot();
            return ChangeItemDurability(1);
        }
        else
        {
            return ThrowItem(1000f);
        }
    }

    void UseMelee()
    {
        meleeParticle.Play();
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
