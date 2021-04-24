using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    public Animator spriteAnimator;
    public float deltaX;
    public float deltaY;

    private float timerMax = 3;
    private float timer;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if ( timer > 0)
        {
            Vector3 newPosition = new Vector3();
            timer -= Time.deltaTime;
            newPosition.y = startPosition.y + ((timerMax - timer) / timerMax) * deltaY;
            newPosition.x = startPosition.x; 

            gameObject.transform.position = newPosition;
        }
    }

    public void PlayBubble()
    {
        timer = timerMax;
        startPosition = gameObject.transform.position;
        spriteAnimator.SetTrigger("Play");
    }
}
