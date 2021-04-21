using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatTrigger : MonoBehaviour
{
    public CheatType type;
    public int level;
    public DoorController door;
    void Start()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Tags.PLAYER))
        {
            door.GiveCheat(level, type);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(Tags.PLAYER))
        {
            door.GiveCheat(level, type);
        }
    }
}
