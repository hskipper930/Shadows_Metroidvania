using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }
    

   
}
