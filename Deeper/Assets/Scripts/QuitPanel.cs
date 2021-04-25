using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitPanel : MonoBehaviour
{
    public GameManager gameManager;
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
                gameManager.CloseQuitPanel();
            }
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            gameManager.CloseQuitPanel();
        }

        yesHighlight.SetActive(yesHighlighted);
        noHighlight.SetActive(!yesHighlighted);
    }
}
