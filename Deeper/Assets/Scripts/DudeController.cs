using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeController : MonoBehaviour
{
    private Rigidbody2D dudeRigidBody;

    public float moveSpeed;
    public float jumpSpeed;

    public float waterMoveSpeed;
    public float waterJumpSpeed;

    public Animator spriteAnimator;
    public SpriteRenderer spriteRenderer;

    private bool isOnGround = true;
    private bool isUnderWater = false;

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
    
    // Start is called before the first frame update
    void Start()
    {
        dudeRigidBody = GetComponent<Rigidbody2D>();
        bubbleTimerCountDown = bubbleTimer;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Physics Update
    private void FixedUpdate()
    {
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
            dudeRigidBody.gravityScale = 0.25f;

            Vector2 movementInput = dudeRigidBody.velocity;

            //Jumping
            float jumpMovement = Input.GetAxis("Jump");

            if (jumpMovement != 0)
            {
                movementInput.y = jumpMovement * waterJumpSpeed;
            }

            //Running
            float horizontalMovement = Input.GetAxis("Horizontal");
            movementInput.x = horizontalMovement * waterMoveSpeed;

            dudeRigidBody.velocity = movementInput;

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
        else
        {
            dudeRigidBody.gravityScale = 1;

            Vector2 movementInput = dudeRigidBody.velocity;

            //Jumping
            float jumpMovement = Input.GetAxis("Jump");

            if (jumpMovement != 0 && isOnGround)
            {
                isOnGround = false;
                movementInput.y = jumpMovement * jumpSpeed;
            }


            //Running
            float horizontalMovement = Input.GetAxis("Horizontal");
            movementInput.x = horizontalMovement * moveSpeed;

            dudeRigidBody.velocity = movementInput;

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
}
