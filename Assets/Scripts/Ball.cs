using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 lastVelocity;
    public Rigidbody rigBody;

    public bool isLayerTwo;
    private void Update()
    {
        lastVelocity = rigBody.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {


        /* Jetzt IN WallBouncern
        if(collision.gameObject.GetComponent<WallBounce>() != null)
        {
            WallBounce wallBounce = collision.gameObject.GetComponent<WallBounce>();
            
            var speed = lastVelocity.magnitude;
            //var direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            var direction = collision.transform.position + transform.position; 
            rigBody.velocity = direction * speed * wallBounce.speedMultiplier;
        }
        */
    }
}
