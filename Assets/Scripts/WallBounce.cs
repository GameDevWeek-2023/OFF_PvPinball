using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBounce : MonoBehaviour
{

    [SerializeField] Renderer GlowRenderer;
    Material GlowMaterial;

    public float speedMultiplier = 1.5f;
    public float standertForce = 10f;

    private void OnCollisionEnter(Collision collision)
    {


            var rigidbody = collision.collider.attachedRigidbody;
            rigidbody.AddForce((standertForce + rigidbody.velocity.magnitude * speedMultiplier) * (rigidbody.position - transform.position).normalized);

    }


}
