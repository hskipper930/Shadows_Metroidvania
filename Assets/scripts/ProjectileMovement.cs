using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    public Vector2 targetPosition;

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, targetPosition);
        float interpolant = (speed * Time.deltaTime) / distance;
        transform.position = Vector2.Lerp(transform.position, targetPosition, interpolant);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //damage player
        }
        Destroy(gameObject);
    }
}
