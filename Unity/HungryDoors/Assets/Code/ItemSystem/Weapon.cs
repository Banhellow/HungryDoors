using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public ParticleSystem shootParticle;


    private void Start()
    {

    }

    public override void Use()
    {
        shootParticle.Play();
        base.Use();
    }


}
