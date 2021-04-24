using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Rigidbody2D cameraRigidBody;

    public float moveForce;
    public float maxMoveSpeed;
    public float followDistance;
    public GameObject followTarget;

    // Use this for initialization
    void Start()
    {

    }

    // Controller is awake
    private void Awake()
    {
        cameraRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Physics Update
    private void FixedUpdate()
    {
        Vector3 followTargetPositionDelta = followTarget.transform.position - transform.position;
        followTargetPositionDelta.z = 0;

        if (followTargetPositionDelta.magnitude <= followDistance)
        {
            cameraRigidBody.velocity = new Vector2(0, 0);
        }
        else
        {
            Vector2 movementDirection = new Vector2(followTarget.transform.position.x - transform.position.x, followTarget.transform.position.y - transform.position.y);

            cameraRigidBody.AddForce(movementDirection * moveForce);

            if (Mathf.Abs(cameraRigidBody.velocity.magnitude) > maxMoveSpeed)
            {
                cameraRigidBody.velocity = cameraRigidBody.velocity.normalized * maxMoveSpeed;
            }
        }
    }
}
