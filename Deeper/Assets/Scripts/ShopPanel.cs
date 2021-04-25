using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    public GameManager gameManager;

    public ShopItem[] shopItems;
    public GameObject exitHighlight;

    private bool exitHighlighted = false;
    private int itemHighlighted = 0;

    // Start is called before the first frame update
    void Start()
    {
        shopItems[0].SetCost(100);
        shopItems[0].SetItemName("Heart Container");

        shopItems[1].SetCost(200);
        shopItems[1].SetItemName("Breathing Tank");
        shopItems[2].SetCost(300);
        shopItems[2].SetItemName("Rebreather");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Left"))
        {
            if (exitHighlighted)
            {
                //DO NOTHING
            }
            else
            {
                itemHighlighted--;
                if (itemHighlighted < 0)
                {
                    itemHighlighted = 0;
                }
            }
        }
        else if (Input.GetButtonDown("Right"))
        {
            if (exitHighlighted)
            {
                //DO NOTHING
            }
            else
            {
                itemHighlighted++;
                if (itemHighlighted >= shopItems.Length)
                {
                    itemHighlighted = shopItems.Length - 1;
                }
            }
        }
        else if (Input.GetButtonDown("Down"))
        {
            if (exitHighlighted)
            {
                //DO NOTHING
            }
            else
            {
                exitHighlighted = true;
            }
        }
        else if (Input.GetButtonDown("Up"))
        {
            if (exitHighlighted)
            {
                exitHighlighted = false;
            }
            else
            {
                //DO NOTHING
            }
        }
        else if (Input.GetButtonDown("Submit"))
        {
            if (exitHighlighted)
            {
                gameManager.CloseShop();
            }
            else
            {
                //BUYING LOGIC
            }
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            gameManager.CloseShop();
        }

        if(exitHighlighted)
        {
            exitHighlight.SetActive(true);
            for (int i = 0; i < shopItems.Length; i++)
            {
                shopItems[i].SetHighlight(false);
            }
        }
        else
        {
            exitHighlight.SetActive(false);
            for (int i = 0; i < shopItems.Length; i++)
            {
                shopItems[i].SetHighlight(i == itemHighlighted);
            }
        }
    }
}
