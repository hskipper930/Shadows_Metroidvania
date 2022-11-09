using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State { patrol, chase}
    private State currentState;
    [SerializeField] private float alertRadius;
    private Transform playerTransform;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private Transform[] checkpoints;
    private int nextCheckpointIndex = 0;

    private void Start()
    {
        currentState = State.patrol;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
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

    private void Move(Vector2 targetPosition, float speed)
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
