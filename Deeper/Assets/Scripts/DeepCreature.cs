using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeepCreature : MonoBehaviour
{
    public float moveSpeed;
    public SpriteRenderer spriteRenderer;
    public int attack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Avoid()
    {
        moveSpeed = -moveSpeed;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    public int GetAttack()
    {
        return attack;
    }
}
