using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeepCreature : MonoBehaviour
{
    public float moveSpeed;
    public SpriteRenderer spriteRenderer;
    public int attack;
    public int maxHealth;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
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

    public int GetHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}
