using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Hunter Skipper
 * SGD 285.2144
 * 12/5/2022 */
public class RangedEnemyAI : EnemyAI
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float shootCooldown;

    protected override void Start()
    {
        base.Start();
        animator.SetBool("attacking", false);
    }

    protected override void Update()
    {
        if (currentState == State.chase)
        {

            if (Vector2.Distance(transform.position, playerTransform.position) > 15)
            {
                if (playerTransform.position.x > transform.position.x)
                {
                    Move(new Vector2(playerTransform.position.x - 15, playerTransform.position.y), chaseSpeed);
                }
                else
                {
                    Move(new Vector2(playerTransform.position.x + 15, playerTransform.position.y), chaseSpeed);
                }
            }
            else
            {
                currentState = State.shooting;
                animator.SetBool("attacking", true);
                StartCoroutine("Shoot");
            }
        }
        if(currentState == State.shooting)
        {
            if (Vector2.Distance(transform.position, playerTransform.position) > 15)
            {
                StopCoroutine("Shoot");
                animator.SetBool("attacking", false);
                currentState = State.chase;
            }
        }
        if (currentState == State.patrol)
        {
            Move(checkpoints[nextCheckpointIndex].position, patrolSpeed);
            if (Vector2.Distance(transform.position, playerTransform.position) <= alertRadius)
            {
                currentState = State.shooting;
                animator.SetBool("attacking", true);
                StartCoroutine("Shoot");
            }
        }
    }

    private IEnumerator Shoot()
    {
        SetHorizontalAnimationParameter(playerTransform.position);
        WaitForSeconds cooldown = new WaitForSeconds(shootCooldown);
        for (; ; )
        {
            ProjectileMovement projectileInstance;
            if(playerTransform.position.x <= transform.position.x)
            {
                projectileInstance = Instantiate(projectile, new Vector2(transform.position.x - 3, transform.position.y), transform.rotation).GetComponent<ProjectileMovement>();
                projectileInstance.targetPosition = new Vector2(transform.position.x - 50, transform.position.y);
            }
            else
            {
                projectileInstance = Instantiate(projectile, new Vector2(transform.position.x + 3, transform.position.y), transform.rotation).GetComponent<ProjectileMovement>();
                projectileInstance.targetPosition = new Vector2(transform.position.x + 50, transform.position.y);
            }
            yield return cooldown;
        }
    }

}
