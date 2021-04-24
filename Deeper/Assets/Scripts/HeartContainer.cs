using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartContainer : MonoBehaviour
{
    public Sprite[] heartSprites;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealth(int health)
    {
        if (health >= 0 && health < heartSprites.Length)
        {
            Image imageComponent = gameObject.GetComponent<Image>();
            imageComponent.sprite = heartSprites[health];
        }
    }
}
