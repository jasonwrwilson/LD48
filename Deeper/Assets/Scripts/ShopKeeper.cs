using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public Animator spriteAnimator;

    public enum ShopType
    {
        ItemShop,
        WeaponShop
    }

    public ShopType shopType;

    bool waving = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Wave(bool flag)
    {
        if (waving != flag)
        {
            waving = flag;
            if ( waving)
            {
                spriteAnimator.SetTrigger("Waving");
            }
            else
            {
                spriteAnimator.SetTrigger("Idle");
            }
        }
    }

    public ShopType GetShopType()
    {
        return shopType;
    }
}
