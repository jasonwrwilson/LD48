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

    private bool isOnGround = true;
    private bool isUnderWater = false;
    
    // Start is called before the first frame update
    void Start()
    {
        dudeRigidBody = GetComponent<Rigidbody2D>();
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
}
