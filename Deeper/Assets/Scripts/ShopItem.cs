using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public GameObject highlight;
    public Text nameText;
    public Text costText;

    private int cost;

    // Start is called before the first frame update
    void Start()
    {
        SetHighlight(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHighlight(bool h)
    {
        if (h)
        {
            highlight.SetActive(true);
        }
        else
        {
            highlight.SetActive(false);
        }
    }

    public void SetItemName(string n)
    {
        nameText.text = n;
    }

    public void SetCost(int c)
    {
        costText.text = c.ToString();
        cost = c;
    }
}
