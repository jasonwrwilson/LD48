﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeController : MonoBehaviour
{
    public GameManager gameManager;

    private Rigidbody2D dudeRigidBody;

    public float moveSpeed;
    public float jumpSpeed;
    public float maxFallSpeed;

    public float waterMoveSpeed;
    public float waterJumpSpeed;

    public Animator spriteAnimator;
    public SpriteRenderer spriteRenderer;

    private bool isOnGround = true;
    private bool isUnderWater = false;

    private int healthMax;
    private int currentHealth;
    public int startingHealth;

    public float invulnerabilityTimer;
    private float invulnerabityTimerCountdown = 0;
    private float invulnerabilityFlashTimer;
    private bool damageShock = false;

    private float maxOxygen;
    private float currentOxygen;
    public float startingOxygen;
    public float oxygenRate;

    private int coins = 1000;

    private enum SpriteAnimationState
    {
        Standing,
        Running,
        Jumping,
        Falling
    }

    private SpriteAnimationState spriteAnimationState = SpriteAnimationState.Standing;

    public BubbleController[] bubbles;
    public float bubbleTimer;
    private float bubbleTimerCountDown;
    private int bubbleIndex = 0;

    private bool nextToShop;
    
    // Start is called before the first frame update
    void Start()
    {
        dudeRigidBody = GetComponent<Rigidbody2D>();
        bubbleTimerCountDown = bubbleTimer;

        healthMax = startingHealth;
        currentHealth = healthMax;

        maxOxygen = startingOxygen;
        currentOxygen = maxOxygen;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Physics Update
    private void FixedUpdate()
    {
        if (gameManager.IsPaused())
        {
            return;
        }

        if ( invulnerabityTimerCountdown > 0)
        {
            invulnerabityTimerCountdown -= Time.deltaTime;
            invulnerabilityFlashTimer -= Time.deltaTime;
            if (invulnerabilityFlashTimer < -0.10f)
            {
                invulnerabilityFlashTimer = -invulnerabilityFlashTimer;
            }
            if (invulnerabityTimerCountdown <= 0)
            {
                Color c = spriteRenderer.color;
                c.a = 1.0f;
                invulnerabityTimerCountdown = 0;
                spriteRenderer.color = c;
            }
            else
            {
                float alpha = 0.5f + Mathf.Abs(invulnerabilityFlashTimer) * 5;
                Color c = spriteRenderer.color;
                c.a = alpha;
                spriteRenderer.color = c;
            }
        }

        if (isUnderWater)
        {
            //Bubbles
            bubbleTimerCountDown -= Time.deltaTime;
            if (bubbleTimerCountDown <= 0)
            {
                //start a bubble
                bubbles[bubbleIndex].gameObject.transform.position = gameObject.transform.position;
                bubbles[bubbleIndex].PlayBubble();
                bubbleIndex++;
                if (bubbleIndex >= bubbles.Length)
                {
                    bubbleIndex = 0;
                }

                bubbleTimerCountDown = bubbleTimer;
            }

            //Oxygen
            currentOxygen -= Time.deltaTime * oxygenRate;
            if (currentOxygen <= 0)
            {
                currentOxygen = 0;
            }

            dudeRigidBody.gravityScale = 0.25f;

            Vector2 movementInput = dudeRigidBody.velocity;

            if (damageShock)
            {
                //shock movement
                movementInput.y = waterJumpSpeed;
                damageShock = false;
            }
            else
            {
                //Jumping
                float jumpMovement = Input.GetAxis("Jump");


                if (jumpMovement != 0)
                {
                    if (nextToShop)
                    {
                        //trigger shop
                        gameManager.OpenShop();
                    }
                    else
                    {
                        movementInput.y = jumpMovement * waterJumpSpeed;
                    }
                }

                //Running
                float horizontalMovement = Input.GetAxis("Horizontal");
                movementInput.x = horizontalMovement * waterMoveSpeed;

                //"Swimming" Animations
                if (movementInput.y > 0)
                {
                    SetAnimationState(SpriteAnimationState.Jumping);
                }
                else
                {
                    SetAnimationState(SpriteAnimationState.Falling);
                }

                if (horizontalMovement > 0)
                {
                    spriteRenderer.flipX = false;
                }
                else
                {
                    spriteRenderer.flipX = true;
                }
            }

            if ( movementInput.y < -maxFallSpeed )
            {
                movementInput.y = -maxFallSpeed;
            }

            dudeRigidBody.velocity = movementInput;

        }
        else
        {
            //Oxygen
            if (currentOxygen < maxOxygen)
            {
                currentOxygen = maxOxygen;
            }

            dudeRigidBody.gravityScale = 1;

            Vector2 movementInput = dudeRigidBody.velocity;

            if (damageShock)
            {
                //shock movement
                movementInput.y = jumpSpeed;
                damageShock = false;
            }
            else
            {
                //Jumping
                float jumpMovement = Input.GetAxis("Jump");


                if (jumpMovement != 0 && isOnGround)
                {
                    if (nextToShop)
                    {
                        //trigger shop
                        gameManager.OpenShop();
                    }
                    else
                    {
                        isOnGround = false;
                        movementInput.y = jumpMovement * jumpSpeed;
                    }
                }


                //Running
                float horizontalMovement = Input.GetAxis("Horizontal");
                movementInput.x = horizontalMovement * moveSpeed;

                //Animation
                if (isOnGround)
                {
                    //Running Animations
                    if (horizontalMovement != 0)
                    {
                        SetAnimationState(SpriteAnimationState.Running);
                        if (horizontalMovement > 0)
                        {
                            spriteRenderer.flipX = false;
                        }
                        else
                        {
                            spriteRenderer.flipX = true;
                        }
                    }
                    else
                    {
                        SetAnimationState(SpriteAnimationState.Standing);
                    }
                }
                else
                {
                    //Jumping Animations
                    if (movementInput.y > 0)
                    {
                        SetAnimationState(SpriteAnimationState.Jumping);
                    }
                    else
                    {
                        SetAnimationState(SpriteAnimationState.Falling);
                    }

                    if (horizontalMovement > 0)
                    {
                        spriteRenderer.flipX = false;
                    }
                    else
                    {
                        spriteRenderer.flipX = true;
                    }
                }
            }

            if (movementInput.y < -maxFallSpeed)
            {
                movementInput.y = -maxFallSpeed;
            }

            dudeRigidBody.velocity = movementInput;

        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        DeepTile collidingTile = collision.gameObject.GetComponent<DeepTile>();

        if (collidingTile != null)
        {
            if (collidingTile.isWater)
            {
                isUnderWater = true;
            }
            else if (collidingTile.isAir)
            {
                isUnderWater = false;
            }
            else
            {
                isOnGround = true;
            }
        }
    }

    private void SetAnimationState(SpriteAnimationState state)
    {
        if (spriteAnimationState != state)
        {
            spriteAnimationState = state;
            switch(spriteAnimationState)
            {
                case SpriteAnimationState.Standing:
                {
                    spriteAnimator.SetTrigger("Standing");
                    break;
                }
                case SpriteAnimationState.Running:
                {
                    spriteAnimator.SetTrigger("Running");
                    break;
                }
                case SpriteAnimationState.Jumping:
                {
                    spriteAnimator.SetTrigger("Jumping");
                    break;
                }
                case SpriteAnimationState.Falling:
                {
                    spriteAnimator.SetTrigger("Falling");
                    break;
                }
            }
        }
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return healthMax;
    }

    public float GetMaxOxygen()
    {
        return maxOxygen;
    }

    public float GetCurrentOxygen()
    {
        return currentOxygen;
    }

    public void TakeDamage(int dmg)
    {
        if (invulnerabityTimerCountdown == 0)
        {
            currentHealth -= dmg;
            invulnerabityTimerCountdown = invulnerabilityTimer;
            invulnerabilityFlashTimer = 0.1f;
            damageShock = true;
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
        }
    }

    public int GetCoins()
    {
        return coins;
    }

    public void SpendCoins(int amount)
    {
        coins -= amount;
        if ( coins < 0 )
        {
            coins = 0;
        }
    }

    public void EarnCoins(int amount)
    {
        coins += amount;
    }

    public void NextToShop(bool flag)
    {
        nextToShop = flag;
    }
}
