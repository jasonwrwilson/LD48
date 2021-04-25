using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : DeepCreature
{
    private Rigidbody2D fishRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        fishRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (!IsDead())
        {
            Vector2 movementInput = fishRigidBody.velocity;

            //Swimming
            movementInput.x = moveSpeed;

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
