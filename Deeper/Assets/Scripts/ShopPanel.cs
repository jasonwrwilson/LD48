using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    public GameManager gameManager;
    public UpgradeManager upgradeManager;

    public ShopItem[] shopItems;
    public GameObject exitHighlight;

    private bool exitHighlighted = false;
    private int itemHighlighted = 0;

    // Start is called before the first frame update
    void Start()
    {
        shopItems[0].SetItemName("Heart Container");
        shopItems[1].SetItemName("Breathing Tank");
        shopItems[2].SetItemName("Flippers");
        shopItems[3].SetItemName("Rebreather");
    }

    // Update is called once per frame
    void Update()
    {
        if (upgradeManager.HasHealthUpgrade())
        {
            shopItems[0].SetCost(upgradeManager.GetHealthUpgradeCost());
        }
        else
        {
            shopItems[0].SetItemName("Sold Out");
        }

        if (upgradeManager.HasTankUpgrade())
        {
            shopItems[1].SetCost(upgradeManager.GetTankUpgradeCost());
        }
        else
        {
            shopItems[1].SetItemName("Sold Out");
        }

        if (upgradeManager.HasFlipperUpgrade())
        {
            shopItems[2].SetCost(upgradeManager.GetFlipperUpgradeCost());
        }
        else
        {
            shopItems[2].SetItemName("Sold Out");
        }

        if (upgradeManager.HasRebreatherUpgrade())
        {
            shopItems[3].SetCost(upgradeManager.GetRebreatherUpgradeCost());
        }
        else
        {
            shopItems[3].SetItemName("Sold Out");
        }

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
                if (itemHighlighted == 0)
                {
                    upgradeManager.UpgradeHealth();
                }
                else if (itemHighlighted == 1)
                {
                    upgradeManager.UpgradeTank();
                }
                else if (itemHighlighted == 2)
                {
                    upgradeManager.UpgradeFlipper();
                }
                else if (itemHighlighted == 3)
                {
                    upgradeManager.UpgradeRebreather();
                }
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
