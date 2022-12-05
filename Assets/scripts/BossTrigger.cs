using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Steven Thompson & Hunter Skipper
 * SGD 285.2144
 * 12/5/2022 */
public class BossTrigger : MonoBehaviour
{
    public Door door;
    public GameObject bossEnemy;
    public BossAI boss;
    [SerializeField] private AudioManager audio;
    // Start is called before the first frame update
    void Start()
    {
        bossEnemy.SetActive(false);
    }

    private void Update()
    {
        if (door.numEnemies <= 0)
        {

            door.roomClear = true;
            door.roomCheck();

            this.gameObject.SetActive(false);


        }
    }
    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            door.roomClear = false;
            door.roomCheck();
            bossEnemy.SetActive(true);
            boss.StartCoroutine("PhaseChanger");
            audio.PlayBossTheme();
            this.gameObject.SetActive(false);
            
        }
    }
}
