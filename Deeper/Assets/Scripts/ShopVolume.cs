using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopVolume : MonoBehaviour
{
    public ShopKeeper shopKeeper;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        DudeController dude = collision.gameObject.GetComponent<DudeController>();

        if (dude != null)
        {
            dude.NextToShop(true);
        }

        shopKeeper.Wave(true);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        DudeController dude = collision.gameObject.GetComponent<DudeController>();

        if (dude != null)
        {
            dude.NextToShop(false);
        }

        shopKeeper.Wave(false);
    }
}
