using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeController : MonoBehaviour
{
    private Rigidbody2D dudeRigidBody;

    public float moveForce;
    public float maxSpeed;
    
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
        Vector2 movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (movementInput.magnitude == 0)
        {
            dudeRigidBody.velocity = new Vector2(0, 0);
        }
        else
        {

            dudeRigidBody.AddForce(movementInput * moveForce);

            if (Mathf.Abs(dudeRigidBody.velocity.magnitude) > maxSpeed)
            {
                dudeRigidBody.velocity = dudeRigidBody.velocity.normalized * maxSpeed;
            }
        }
    }
}
