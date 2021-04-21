using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatTrigger : MonoBehaviour
{
    public CheatType type;
    public int level;
    Conversation conversation;
    void Start()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Tags.PLAYER))
        {
            

        }
    }
}
