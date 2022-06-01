using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimationEvents : MonoBehaviour
{
    public DoorController door;
    void Start()
    {
        
    }
    
    public void SpawnEnemyEvent()
    {
        door.SpawnEnemy();
    }
}
