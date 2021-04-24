using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class O2Meter : MonoBehaviour
{
    public Sprite[] O2Sprites;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetOxygen(int oxygen)
    {
        if (oxygen >= 0 && oxygen < O2Sprites.Length)
        {
            Image imageComponent = gameObject.GetComponent<Image>();
            imageComponent.sprite = O2Sprites[oxygen];
        }
    }
}
