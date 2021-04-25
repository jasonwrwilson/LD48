using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isPaused = false;

    public GameObject shopPanel;
    
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
}
