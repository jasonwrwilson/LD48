using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public DudeController dude;

    public int[] healthUpgradeCosts;
    public int[] tankUpgradeCosts;
    public int[] flipperUpgradeCosts;
    public int[] rebreatherUpgradeCosts;

    private int healthUpgradeNum = 0;
    private int tankUpgradeNum = 0;
    private int flipperUpgradeNum = 0;
    private int rebreatherUpgradeNum = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeHealth()
    {
        if (dude.GetCoins() >= GetHealthUpgradeCost())
        {
            dude.SpendCoins(GetHealthUpgradeCost());
            dude.UpgradeHealth();
            healthUpgradeNum++;
        }    
    }

    public void UpgradeTank()
    {
        if (dude.GetCoins() >= GetTankUpgradeCost())
        {
            dude.SpendCoins(GetTankUpgradeCost());
            dude.UpgradeTank();
            tankUpgradeNum++;
        }

    }

    public void UpgradeFlipper()
    {
        if (dude.GetCoins() >= GetFlipperUpgradeCost())
        {
            dude.SpendCoins(GetFlipperUpgradeCost());
            dude.UpgradeFlipper();
            flipperUpgradeNum++;
        }      
    }

    public void UpgradeRebreather()
    {
        if (dude.GetCoins() >= GetRebreatherUpgradeCost())
        {
            dude.SpendCoins(GetRebreatherUpgradeCost());
            dude.UpgradeRebreather();
            rebreatherUpgradeNum++;
        }
    }

    public int GetHealthUpgradeCost()
    {
        return healthUpgradeCosts[healthUpgradeNum];
    }

    public int GetTankUpgradeCost()
    {
        return tankUpgradeCosts[tankUpgradeNum];
    }

    public int GetFlipperUpgradeCost()
    {
        return flipperUpgradeCosts[flipperUpgradeNum];
    }

    public int GetRebreatherUpgradeCost()
    {
        return rebreatherUpgradeCosts[rebreatherUpgradeNum];
    }

    public bool HasHealthUpgrade()
    {
        return healthUpgradeNum < healthUpgradeCosts.Length;
    }

    public bool HasTankUpgrade()
    {
        return tankUpgradeNum < tankUpgradeCosts.Length;
    }

    public bool HasFlipperUpgrade()
    {
        return flipperUpgradeNum < flipperUpgradeCosts.Length;
    }

    public bool HasRebreatherUpgrade()
    {
        return rebreatherUpgradeNum < rebreatherUpgradeCosts.Length;
    }


}
