using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public ParticleSystem shootParticle;


    private void Start()
    {
        data.type = ItemType.Weapon;
    }

    public override Item Use()
    {
        return this;
    }


}
