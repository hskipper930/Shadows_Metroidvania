using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Hunter Skipper
 * SGD 285.2144
 * 12/5/2022 */
public class BossAI : EnemyAI
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float timeBetweenPhases;

    protected override void Start()
    {
        base.Start();
        currentState = State.chase;
        StartCoroutine("PhaseChanger");
    }

    private void OnEnable()
    {
        currentState = State.chase;
        StartCoroutine("PhaseChanger");
    }

    protected override void Update()
    {
        base.Update();
        if(currentState == State.shooting)
        {
            Move(new Vector2(playerTransform.position.x, playerTransform.position.y + 10), chaseSpeed);
        }
    }

    private IEnumerator PhaseChanger()
    {
        WaitForSeconds interval = new WaitForSeconds(timeBetweenPhases);
        for(; ; )
        {
            yield return interval;
            currentState = State.shooting;
            StartCoroutine("Shoot");
            yield return interval;
            currentState = State.chase;
            StopCoroutine("Shoot");
        }
    }

    private IEnumerator Shoot()
    {
        WaitForSeconds cooldown = new WaitForSeconds(shootCooldown);
        for (; ; )
        {
            ProjectileMovement projectileInstance;
            if (playerTransform.position.x <= transform.position.x)
            {
                projectileInstance = Instantiate(projectile, new Vector2(transform.position.x - 3, transform.position.y), transform.rotation).GetComponent<ProjectileMovement>();
                projectileInstance.targetPosition = playerTransform.position;
            }
            else
            {
                projectileInstance = Instantiate(projectile, new Vector2(transform.position.x + 3, transform.position.y), transform.rotation).GetComponent<ProjectileMovement>();
                projectileInstance.targetPosition = playerTransform.position;
            }
            yield return cooldown;
        }
    }

    protected override void TakeDamage(int amount)
    {
        audio.PlayEnemyDamagedSound();
        health -= amount;
        if (health <= 0)
        {
            door.numEnemies--;
            audio.PlayMainTheme();
            Destroy(gameObject);
        }
    }
}
