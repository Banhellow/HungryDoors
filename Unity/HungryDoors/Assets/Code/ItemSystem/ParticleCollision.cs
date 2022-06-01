using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    public ParticleSystem shootParticle;
    public float pushbackForce;
    private List<ParticleCollisionEvent> collisionEvents;
    void Start()
    {
        shootParticle = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision(GameObject other)
    {
        int collisionEventsCount = shootParticle.GetCollisionEvents(other, collisionEvents);
        Rigidbody rb = other.GetComponent<Rigidbody>();
        int i = 0;
        while (i < collisionEventsCount)
        {
            if (rb)
            {
                Vector3 pos = collisionEvents[i].intersection;
                Vector3 force = collisionEvents[i].velocity * pushbackForce;
                rb.AddForce(force);
            }
            i++;
        }
    }
}
