using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    protected enum State { patrol, chase, shooting}
    protected State currentState;
    [SerializeField] protected float alertRadius;
    protected Transform playerTransform;
    [SerializeField] protected float patrolSpeed;
    [SerializeField] protected float chaseSpeed;
    [SerializeField] protected Transform[] checkpoints;
    protected int nextCheckpointIndex = 0;

    private void Start()
    {
        currentState = State.patrol;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        if (currentState == State.chase)
        {
            Move(playerTransform.position, chaseSpeed);
        }

        if(currentState == State.patrol)
        {
            Move(checkpoints[nextCheckpointIndex].position, patrolSpeed);
            if(Vector2.Distance(transform.position, playerTransform.position) <= alertRadius)
            {
                currentState = State.chase;
            }
        }
    }

    protected void Move(Vector2 targetPosition, float speed)
    {
        float distance = Vector2.Distance(transform.position, targetPosition);
        float interpolant = (speed * Time.deltaTime) / distance;
        transform.position = Vector2.Lerp(transform.position, targetPosition, interpolant);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("checkpoint"))
        {
            if(nextCheckpointIndex == checkpoints.Length - 1)
            {
                nextCheckpointIndex = 0;
            }
            else
            {
                nextCheckpointIndex++;
            }
        }
    }
}
