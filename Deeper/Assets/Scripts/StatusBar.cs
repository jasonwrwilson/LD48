using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public DudeController dude;
    public HeartContainer[] heartContainers;
    public O2Meter[] O2Containers;

    private int lastHealth = 0;
    private int lastMaxHealth = 0;

    private float lastOxygen = 0;
    private float lastMaxOxygen = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Update Health Level
        int currentHealth = dude.GetHealth();
        int currentMaxHealth = dude.GetMaxHealth();
        if ( currentHealth != lastHealth || currentMaxHealth != lastMaxHealth )
        {
            for ( int i = 0; i < heartContainers.Length; i++)
            {
                if (currentMaxHealth >= (i + 1) * 2)
                {
                    heartContainers[i].gameObject.SetActive(true);

                    if (currentHealth >= (i + 1) * 2)
                    {
                        heartContainers[i].SetHealth(2);
                    }
                    else if (currentHealth >= i * 2 + 1)
                    {
                        heartContainers[i].SetHealth(1);
                    }
                    else
                    {
                        heartContainers[i].SetHealth(0);
                    }
                }
                else
                {
                    heartContainers[i].gameObject.SetActive(false);
                }
            }
        }

        //Update Oxygen Levels
        float currentOxygen = dude.GetCurrentOxygen();
        float currentMaxOxygen = dude.GetMaxOxygen();
        if (currentOxygen != lastOxygen || currentMaxOxygen != lastMaxOxygen)
        {
            for (int i = 0; i < O2Containers.Length; i++)
            {
                if (currentMaxOxygen > i)
                {
                    O2Containers[i].gameObject.SetActive(true);

                    if (currentOxygen > i)
                    {
                        O2Containers[i].SetOxygen(1);
                    }
                    else
                    {
                        O2Containers[i].SetOxygen(0);
                    }
                }
                else
                {
                    O2Containers[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
