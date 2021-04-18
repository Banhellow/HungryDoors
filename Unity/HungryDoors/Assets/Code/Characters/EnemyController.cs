using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState { Idle, Patrol, Chase, Fight, Dead }

public class EnemyController : Character
{
    public EnemyState currentState;



    [Header("Partol")]
    public List<Transform> partolPoints;
    private int currentPartolPoint = 0;

    [Header("Idle")]
    public float idleTime = 1;
    private float idleTimer = 0;

    [Header("Movement")]
    public Transform movementGoal;
    NavMeshAgent agent;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        if (currentState == EnemyState.Patrol)
            SetMovementGoal(partolPoints[currentPartolPoint % partolPoints.Count]);
    }

    void Update()
    {
        agent.destination = movementGoal.position;

        if (currentState == EnemyState.Idle)
        {
            idleTimer += Time.deltaTime;
            if(idleTimer >= idleTime)
            {
                SetNextPatrolPoint();
            }
        }

        if (agent.isStopped == false && agent.remainingDistance < 3f)
        {
            Debug.Log("3m dist");
            if (currentState == EnemyState.Patrol)
            {
                StartIdleState();
            }
            else
            {
                StopAgentMovement();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.PLAYER))
        {
            currentState = EnemyState.Chase;
            SetMovementGoal(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.PLAYER))
        {
            SetNextPatrolPoint();
        }
    }

    #region Movement

    private void SetMovementGoal(Transform goalTR)
    {
        if (movementGoal == goalTR)
            return;

        movementGoal = goalTR;
    }

    [Button]
    private void StopAgentMovement()
    {
        agent.isStopped = true;
        animator.SetBool(isMovingParam, false);
    }

    [Button]
    private void StartAgentMovement()
    {
        agent.isStopped = false;
        animator.SetBool(isMovingParam, true);
    }

    #endregion Movement

    #region Patrol

    private void SetNextPatrolPoint()
    {
        currentPartolPoint++;
        SetMovementGoal(partolPoints[currentPartolPoint % partolPoints.Count]);
        currentState = EnemyState.Patrol;
        StartAgentMovement();
    }

    #endregion Patrol

    private void StartIdleState()
    {
        idleTimer = 0;
        currentState = EnemyState.Idle;
    }

    public override void Die()
    {
        base.Die();
        Debug.Log($"Enemy {gameObject.name} dead.");
    }
}
