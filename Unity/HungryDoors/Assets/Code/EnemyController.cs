using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Character
{
    [Header("Movement")]
    public Transform movementGoal;
    NavMeshAgent agent;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        //if (agent.remainingDistance < 1f)
        //{
        //    StopAgentMovement();
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.PLAYER))
        {
            movementGoal = other.transform;
            agent.destination = movementGoal.position;
        }
    }

    [Button]
    private void StopAgentMovement()
    {
        agent.isStopped = true;
    }

    [Button]
    private void StartAgentMovement()
    {
        agent.isStopped = false;
    }

    public override void Die()
    {
        base.Die();
        Debug.Log($"Enemy {gameObject.name} dead.");
    }
}
