using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    public int gold;

    public SpriteRenderer spriteRenderer;
    public Sprite[] chestSprites;

    public float emptyTimer;
    private float emptyTimerCountDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (emptyTimerCountDown > 0)
        {
            emptyTimerCountDown -= Time.deltaTime;

            if (emptyTimerCountDown <= 0)
            {
                spriteRenderer.sprite = chestSprites[2];
            }
        }
    }

    public void SetGold(int amount)
    {
        gold = amount;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        DudeController dude = collision.gameObject.GetComponent<DudeController>();

        if (dude != null && gold > 0)
        {
            dude.EarnCoins(gold);
            gold = 0;
            spriteRenderer.sprite = chestSprites[1];

            emptyTimerCountDown = emptyTimer;
        }
    }
}
