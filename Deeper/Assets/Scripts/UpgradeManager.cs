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

    public int[] attackUpgradeCosts;
    public int[] shotCountUpgradeCosts;
    public int[] firingSpeedCosts;
    public int[] multiShotCosts;

    private int healthUpgradeNum = 0;
    private int tankUpgradeNum = 0;
    private int flipperUpgradeNum = 0;
    private int rebreatherUpgradeNum = 0;

    private int attackUpgradeNum = 0;
    private int shotCountUpgradeNum = 0;
    private int firingSpeedUpgradeNum = 0;
    private int multiShotUpgradeNum = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        ResetUpgrades();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetUpgrades()
    {
        healthUpgradeNum = 0;
        tankUpgradeNum = 0;
        flipperUpgradeNum = 0;
        rebreatherUpgradeNum = 0;

        attackUpgradeNum = 0;
        shotCountUpgradeNum = 0;
        firingSpeedUpgradeNum = 0;
        multiShotUpgradeNum = 0;
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

    public void UpgradeAttack()
    {
        if (dude.GetBones() >= GetAttackUpgradeCost())
        {
            dude.SpendBones(GetAttackUpgradeCost());
            dude.UpgradeAttack();
            attackUpgradeNum++;
        }
    }

    public void UpgradeShotCount()
    {
        if (dude.GetBones() >= GetShotCountUpgradeCost())
        {
            dude.SpendBones(GetShotCountUpgradeCost());
            dude.UpgradeShotCount();
            shotCountUpgradeNum++;
        }
    }

    public void UpgradeFiringSpeed()
    {
        if (dude.GetBones() >= GetFiringSpeedUpgradeCost())
        {
            dude.SpendBones(GetFiringSpeedUpgradeCost());
            dude.UpgradeFiringSpeed();
            firingSpeedUpgradeNum++;
        }
    }

    public void UpgradeMultishot()
    {
        if (dude.GetBones() >= GetMultiShotUpgradeCost())
        {
            dude.SpendBones(GetMultiShotUpgradeCost());
            dude.UpgradeMultiShot();
            multiShotUpgradeNum++;
        }
    }

    public int GetAttackUpgradeCost()
    {
        return attackUpgradeCosts[attackUpgradeNum];
    }

    public int GetShotCountUpgradeCost()
    {
        return shotCountUpgradeCosts[shotCountUpgradeNum];
    }

    public int GetMultiShotUpgradeCost()
    {
        return multiShotCosts[multiShotUpgradeNum];
    }

    public int GetFiringSpeedUpgradeCost()
    {
        return firingSpeedCosts[firingSpeedUpgradeNum];
    }

    public bool HasAttackUpgrade()
    {
        return attackUpgradeNum < attackUpgradeCosts.Length;
    }

    public bool HasShotCountUpgrade()
    {
        return shotCountUpgradeNum < shotCountUpgradeCosts.Length;
    }

    public bool HasMultiShotUpgrade()
    {
        return multiShotUpgradeNum < multiShotCosts.Length;
    }

    public bool HasFiringSpeedUpgradeCost()
    {
        return firingSpeedUpgradeNum < firingSpeedCosts.Length;
    }
}
