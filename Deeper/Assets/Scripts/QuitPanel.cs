using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitPanel : MonoBehaviour
{
    public GameManager gameManager;
    public MenuManager menuManager;
    public GameObject yesHighlight;
    public GameObject noHighlight;

    private bool yesHighlighted = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Left"))
        {
            if (yesHighlighted)
            {
                //DO NOTHING
            }
            else
            {
                yesHighlighted = true;
            }
        }
        else if (Input.GetButtonDown("Right"))
        {
            if (yesHighlighted)
            {
                yesHighlighted = false;
            }
            else
            {
                //DO NOTHING
            }
        }
        else if (Input.GetButtonDown("Submit"))
        {
            if (yesHighlighted)
            {
                Application.Quit();
            }
            else
            {
                if (gameManager != null)
                {
                    gameManager.CloseQuitPanel();
                }
                else if (menuManager != null)
                {
                    menuManager.CloseQuitPanel();
                }
            }
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            if (gameManager != null)
            {
                gameManager.CloseQuitPanel();
            }
            else if (menuManager != null)
            {
                menuManager.CloseQuitPanel();
            }
        }

        yesHighlight.SetActive(yesHighlighted);
        noHighlight.SetActive(!yesHighlighted);
    }
}
