using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject quitPanel;

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            return;
        }

        if (Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene("GameScene");
        }
        else if (Input.GetKeyDown("escape"))
        {
            OpenQuitPanel();
        }
    }

    public void OpenQuitPanel()
    {
        quitPanel.SetActive(true);
        PauseGame(true);
    }

    public void CloseQuitPanel()
    {
        quitPanel.SetActive(false);
        PauseGame(false);
    }

    public void PauseGame(bool pause)
    {
        isPaused = pause;
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
