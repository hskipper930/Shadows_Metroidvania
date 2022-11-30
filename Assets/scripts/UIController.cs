using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject helpPanel; 
    [SerializeField] private GameObject pausePanel;
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("MainLevel");
    }

    public void OnHelpButtonClick()
    {
        helpPanel.SetActive(true);
    }

    public void OnCloseButtonClick()
    {
        helpPanel.SetActive(false);
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
