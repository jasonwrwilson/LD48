using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeController : MonoBehaviour
{
    public GameManager gameManager;

    private ProjectilePool projectilePool;

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

    public int startingCoins;
    private int coins;

    public int startingBones;
    private int bones;

    public int startingAttack;
    private int currentAttack;

    public int startingShotCount;
    private int currentShotCount;

    private int shotsFired = 0;
    public float shotTimer;
    private float shotTimerCountdown;

    public float startingFiringSpeed;
    private float currentFiringSpeed;

    private bool multiShot = false;

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

    private bool nextToShop = false;
    private bool nextToWeapons = false;

    private bool facingLeft = false;

    private Vector3 startingPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        dudeRigidBody = GetComponent<Rigidbody2D>();
        projectilePool = GetComponent<ProjectilePool>();
        startingPosition = transform.position;

        ResetDude();
    }

    public void ResetDude()
    {
        healthMax = startingHealth;
        currentHealth = healthMax;
        maxOxygen = startingOxygen;
        currentOxygen = maxOxygen;
        transform.position = startingPosition;
        bubbleTimerCountDown = bubbleTimer;
        coins = startingCoins;
        bones = startingBones;
        currentAttack = startingAttack;
        currentShotCount = startingShotCount;
        currentFiringSpeed = startingFiringSpeed;
        multiShot = false;
        shotsFired = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.IsPaused())
        {
            return;
        }

        if (shotTimerCountdown > 0)
        {
            shotTimerCountdown -= Time.deltaTime;
            if (shotTimerCountdown <= 0)
            {
                shotsFired = 0;
            }
        }

        //interaction
        if (Input.GetButtonDown("Submit"))
        {
            if (nextToShop)
            {
                //trigger shop
                gameManager.OpenShop();
            }
            else if (nextToWeapons)
            {
                //trigger weapon shop
                gameManager.OpenWeapons();
            }
        }
        else if (Input.GetKeyDown("escape"))
        {
            gameManager.OpenQuitPanel();           
        }

        if (invulnerabityTimerCountdown > 0)
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
                gameManager.Death((int)-transform.position.y - 6, GameManager.DeathType.Air);
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
                if (Input.GetButtonDown("Jump") && !nextToShop && !nextToWeapons)
                {
                    movementInput.y = waterJumpSpeed;
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
                    facingLeft = false;
                }
                else
                {
                    spriteRenderer.flipX = true;
                    facingLeft = true;
                }
            }

            if (movementInput.y < -maxFallSpeed)
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
                if (Input.GetButtonDown("Jump") && isOnGround && !nextToShop && !nextToWeapons)
                {
                    isOnGround = false;
                    movementInput.y = jumpSpeed;
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
                            facingLeft = false;
                        }
                        else
                        {
                            spriteRenderer.flipX = true;
                            facingLeft = true;
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
                        facingLeft = false;
                    }
                    else
                    {
                        spriteRenderer.flipX = true;
                        facingLeft = true;
                    }
                }
            }

            if (movementInput.y < -maxFallSpeed)
            {
                movementInput.y = -maxFallSpeed;
            }

            dudeRigidBody.velocity = movementInput;

        }

        //interaction
        if (Input.GetButtonDown("Fire1"))
        {
            if (shotsFired < currentShotCount)
            {
                int numProjectiles = 1;
                if (multiShot)
                {
                    numProjectiles = 3;
                }

                for (int i = 0; i < numProjectiles; i++)
                {
                    float offset = 0;
                    if (i == 1)
                    {
                        offset = 0.5f;
                    }
                    else if (i == 2)
                    {
                        offset = -0.5f;
                    }

                    float stagger = 0;
                    if (i > 0)
                    {
                        stagger = 0.5f;
                    }

                    Projectile projectile;
                    if (Input.GetAxis("Vertical") > 0)
                    {
                        projectile = projectilePool.GetProjectile(1);

                        Vector3 position = transform.position;
                        position.x += offset;
                        position.y -= stagger;
                        projectile.transform.position = position;
                        projectile.SetDirection(new Vector3(0, currentFiringSpeed, 0));
                    }
                    else if (Input.GetAxis("Vertical") < 0)
                    {
                        projectile = projectilePool.GetProjectile(1);

                        Vector3 position = transform.position;
                        position.x += offset;
                        position.y += stagger;
                        projectile.transform.position = position;
                        projectile.SetDirection(new Vector3(0, -currentFiringSpeed, 0));
                    }
                    else
                    {
                        projectile = projectilePool.GetProjectile(0);

                        if (facingLeft)
                        {
                            Vector3 position = transform.position;
                            position.x += stagger;
                            position.y += offset;
                            projectile.transform.position = position;
                            projectile.SetDirection(new Vector3(-currentFiringSpeed, 0, 0));
                        }
                        else
                        {
                            Vector3 position = transform.position;
                            position.x -= stagger;
                            position.y += offset;
                            projectile.transform.position = position;
                            projectile.SetDirection(new Vector3(currentFiringSpeed, 0, 0));
                        }
                    }

                    projectile.SetAttack(GetAttack());
                    projectile.SetDude(this);
                }

                if (shotsFired == 0)
                {
                    shotTimerCountdown = shotTimer;
                }
                shotsFired++;
            }
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

    public int GetAttack()
    {
        return currentAttack;
    }

    public void TakeDamage(int dmg)
    {
        if (invulnerabityTimerCountdown == 0)
        {
            currentHealth -= dmg;
            invulnerabityTimerCountdown = invulnerabilityTimer;
            invulnerabilityFlashTimer = 0.1f;
            damageShock = true;
            if (currentHealth <= 0)
            {
                currentHealth = 0;

                gameManager.Death((int)-transform.position.y - 6, GameManager.DeathType.Fish);
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

    public int GetBones()
    {
        return bones;
    }

    public void SpendBones(int amount)
    {
        bones -= amount;
        if ( bones < 0 )
        {
            bones = 0;
        }
    }

    public void EarnBones(int amount)
    {
        bones += amount;
    }

    public void NextToShop(bool flag)
    {
        nextToShop = flag;
        gameManager.ShowEnterPrompt(flag);
    }

    public void NextToWeapons(bool flag)
    {
        nextToWeapons = flag;
        gameManager.ShowEnterPrompt(flag);
    }

    public void UpgradeHealth()
    {
        healthMax += 2;
        currentHealth += 2;
    }

    public void UpgradeTank()
    {
        maxOxygen += 2;
        currentOxygen += 2;
    }

    public void UpgradeFlipper()
    {
        waterJumpSpeed *= 2;
        waterMoveSpeed *= 2;
    }

    public void UpgradeRebreather()
    {
        oxygenRate *= 0.5f;
    }

    public void UpgradeAttack()
    {
        currentAttack++;
    }

    public void UpgradeFiringSpeed()
    {
        currentFiringSpeed *= 2;
    }

    public void UpgradeMultiShot()
    {
        multiShot = true;
    }

    public void UpgradeShotCount()
    {
        currentShotCount++;
    }
}
