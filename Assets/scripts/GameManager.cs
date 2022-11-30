using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Movement player;
    public GameObject firstBridge;
    public GameObject pausePanel;
    // Start is called before the first frame update
    void Start()
    {
        firstBridge.SetActive(false);
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.numLogs >= 3)
        {
            firstBridge.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void OnPauseButtonClick()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void OnMenuButtonClick()
    {
        SceneManager.LoadScene("Frontend");
    }
    public void OnRetryButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }


}
