using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject followTarget;
    
    // Use this for initialization
    void Start()
    {

    }

    // Controller is awake
    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Physics Update
    private void FixedUpdate()
    {
        Vector3 pos = followTarget.transform.position;
        pos.z = transform.position.z;
        transform.position = pos;
    }
}
