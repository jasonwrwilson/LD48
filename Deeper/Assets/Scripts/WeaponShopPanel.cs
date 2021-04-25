using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShopPanel : MonoBehaviour
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
        shopItems[0].SetItemName("Spear Damage");
        shopItems[1].SetItemName("Spear Count");
        shopItems[2].SetItemName("Multi-Shot");
        shopItems[3].SetItemName("Throwing Speed");
    }

    // Update is called once per frame
    void Update()
    {
        if (upgradeManager.HasAttackUpgrade())
        {
            shopItems[0].SetCost(upgradeManager.GetAttackUpgradeCost());
        }
        else
        {
            shopItems[0].SetItemName("Sold Out");
        }

        if (upgradeManager.HasShotCountUpgrade())
        {
            shopItems[1].SetCost(upgradeManager.GetShotCountUpgradeCost());
        }
        else
        {
            shopItems[1].SetItemName("Sold Out");
        }

        if (upgradeManager.HasMultiShotUpgrade())
        {
            shopItems[2].SetCost(upgradeManager.GetMultiShotUpgradeCost());
        }
        else
        {
            shopItems[2].SetItemName("Sold Out");
        }

        if (upgradeManager.HasFiringSpeedUpgradeCost())
        {
            shopItems[3].SetCost(upgradeManager.GetFiringSpeedUpgradeCost());
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
                gameManager.CloseWeapons();
            }
            else
            {
                //BUYING LOGIC
                if (itemHighlighted == 0)
                {
                    upgradeManager.UpgradeAttack();
                }
                else if (itemHighlighted == 1)
                {
                    upgradeManager.UpgradeShotCount();
                }
                else if (itemHighlighted == 2)
                {
                    upgradeManager.UpgradeMultishot();
                }
                else if (itemHighlighted == 3)
                {
                    upgradeManager.UpgradeFiringSpeed();
                }
            }
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            gameManager.CloseWeapons();
        }

        if (exitHighlighted)
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
