using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepthGauge : MonoBehaviour
{
    public Text gaugeText;
    public GameObject dude;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int depth = (int)-dude.transform.position.y;
        if ( depth < 0 )
        {
            depth = 0;
        }
        gaugeText.text = depth.ToString() + "m";
    }
}
