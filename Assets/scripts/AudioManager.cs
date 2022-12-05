using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource mainTheme;
    [SerializeField] private AudioSource bossTheme;
    [SerializeField] private AudioSource axeSound;
    [SerializeField] private AudioSource playerDamagedSound;
    [SerializeField] private AudioSource enemyDamagedSound;

    public void PlayMainTheme()
    {
        bossTheme.Stop();
        mainTheme.Play();
    }

    public void PlayBossTheme()
    {
        mainTheme.Stop();
        bossTheme.Play();
    }

    public void PlayAxeSound()
    {
        axeSound.Play();
    }

    public void PlayPlayerDamagedSound()
    {
        playerDamagedSound.Play();
    }

    public void PlayEnemyDamagedSound()
    {
        enemyDamagedSound.Play();
    }
}
