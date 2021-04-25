using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathPanel : MonoBehaviour
{
    public GameManager gameManager;

    public Text quipText;
    public Text currentDepthText;
    public Text recordDepthText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") || Input.GetButton("Cancel"))
        {
            gameManager.CloseDeathPanel();
        }
    }

    public void ShowDeathPanel(string quip, int depth, int record)
    {
        quipText.text = quip;
        currentDepthText.text = "You reached: " + depth.ToString() + "m";
        if (depth > record)
        {
            recordDepthText.text = "New Record!!";
        }
        else
        {
            recordDepthText.text = "Record: " + record.ToString() + "m";
        }
    }
}
