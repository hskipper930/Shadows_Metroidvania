using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/* Steven Thompson & Hunter Skipper
 * SGD 285.2144
 * 12/5/2022 */
public class GameManager : MonoBehaviour
{
    public Movement player;
    public GameObject firstBridge;
    public GameObject pausePanel;
    [SerializeField] private GameObject logsText;
    public GameObject winPanel;
    // Start is called before the first frame update
    void Start()
    {
        firstBridge.SetActive(false);
        pausePanel.SetActive(false);
        winPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.numLogs >= 3)
        {
            firstBridge.SetActive(true);
            logsText.SetActive(false);
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
        Time.timeScale = 1;
    }
    public void GameWin()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void OnRetryButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }


}
