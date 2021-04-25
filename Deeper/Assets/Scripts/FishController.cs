using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : DeepCreature
{
    private Rigidbody2D fishRigidBody;

    public float wiggleTimer;
    private float wiggleTimerCountdown;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        fishRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        wiggleTimerCountdown -= Time.deltaTime;
        if (wiggleTimerCountdown < -wiggleTimer )
        {
            wiggleTimerCountdown = wiggleTimer;
        }

        if (!IsDead())
        {
            Vector2 movementInput = fishRigidBody.velocity;

            //Swimming
            movementInput.x = moveSpeed;

            if (wiggleTimerCountdown > 0)
            {
                movementInput.y = -0.5f;
            }
            else
            {
                movementInput.y = 0.5f;
            }

            fishRigidBody.velocity = movementInput;
        }
        else
        {
            Vector2 movementInput = fishRigidBody.velocity;

            movementInput.x = 0;
            movementInput.y = -1;

            fishRigidBody.velocity = movementInput;
        }
    }
}
