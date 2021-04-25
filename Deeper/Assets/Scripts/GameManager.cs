using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isPaused = false;

    public GameObject shopPanel;
    public GameObject enterPrompt;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
        isPaused = true;
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        isPaused = false;
    }

    public void ShowEnterPrompt(bool show)
    {
        enterPrompt.SetActive(show);
    }
}
