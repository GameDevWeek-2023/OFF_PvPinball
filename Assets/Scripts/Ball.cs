using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 lastVelocity;
    public Rigidbody rigBody;

    public bool isLayerTwo;
    public bool isLeftPlayer = true;
    
    private void Update()
    {
        lastVelocity = rigBody.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //FindObjectOfType<AudioManager>().Play("BallHit");
    }
}
