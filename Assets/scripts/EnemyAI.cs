using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] private float speed;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Move(playerTransform.position);
    }

    private void Move(Vector2 targetPosition)
    {
        float distance = Vector2.Distance(transform.position, targetPosition);
        float interpolant = distance / (speed * Time.deltaTime);
        transform.position = Vector2.Lerp(transform.position, targetPosition, interpolant);
    }
}
