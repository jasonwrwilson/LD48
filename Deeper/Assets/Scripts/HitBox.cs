using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : PlayerTriggerVolume
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
        DeepCreature collidingCreature = collision.gameObject.GetComponent<DeepCreature>();

        if (collidingCreature != null)
        {
            dude.TakeDamage(collidingCreature.GetAttack());
        }
    }
}
