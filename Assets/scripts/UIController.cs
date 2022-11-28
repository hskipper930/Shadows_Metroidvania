using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject helpPanel; 
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

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
