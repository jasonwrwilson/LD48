﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeepCreature : MonoBehaviour
{
    public float moveSpeed;
    public SpriteRenderer spriteRenderer;
    public Animator spriteAnimator;
    public int attack;
    public int maxHealth;
    private int currentHealth;

    public float deathTimer;
    private float deathTimerCountdown;

    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Debug.Log("Calling base update");
        if ( deathTimerCountdown > 0 )
        {
            deathTimerCountdown -= Time.deltaTime;
            if (deathTimerCountdown <= 0 )
            {
                deathTimerCountdown = 0;
                gameObject.SetActive(false);
            }
            Color c = spriteRenderer.color;
            c.a = deathTimerCountdown / deathTimer;

            spriteRenderer.color = c;
        }
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
            spriteAnimator.SetTrigger("Dead");
            deathTimerCountdown = deathTimer;
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        spriteAnimator.SetTrigger("Alive");
        Color c = spriteRenderer.color;

        c.a = 1.0f;
        spriteRenderer.color = c;
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }
}
