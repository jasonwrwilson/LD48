using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidanceTrigger : TriggerVolume
{
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
        DeepTile collidingTile = collision.gameObject.GetComponent<DeepTile>();

        if (collidingTile != null)
        {
            if (!collidingTile.isWater && !collidingTile.isAir)
            {
                creature.Avoid();
            }
        }
    }
}
