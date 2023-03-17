using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WallBounce : MonoBehaviour
{

    [SerializeField] Renderer GlowRenderer;
    Material GlowMaterial;

    public GameObject Rotoren;
    [SerializeField]
    float speed,maxSpeed, minSpeed;

    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color minColor, maxColor;

    public float stateAbfall,stateAufbau , maxPartikleSpawn;
    [SerializeField]
    float curendState,sollState;
    public float speedMultiplier = 1.5f;
    public float standertForce = 10f;
    [SerializeField]
    private VisualEffect visualEffect;
    private void Start()
    {
        visualEffect = GetComponent<VisualEffect>();
    }

    private void OnCollisionEnter(Collision collision)
    {
            var rigidbody = collision.collider.attachedRigidbody;
            rigidbody.AddForce((standertForce + rigidbody.velocity.magnitude * speedMultiplier) * (rigidbody.position - transform.position).normalized);
            sollState = 1;
            FindObjectOfType<AudioManager>().Play("Bumper");
    }
    private void Update()
    {

        if(curendState != sollState)
        {
            if (curendState < sollState)
            {
                curendState += stateAufbau;
                curendState = Math.Clamp(curendState, 0f, 1f);
            }
            else
            {
                curendState -= stateAbfall;
                curendState = Math.Clamp(curendState, 0f, 1f);
            }

            speed = Mathf.Lerp(minSpeed, maxSpeed, curendState);
            GlowRenderer.material.SetColor("_Color_1", Color.Lerp(minColor, maxColor, curendState));
        }

        sollState = Mathf.Clamp01(sollState - stateAbfall);
        Rotoren.transform.localRotation *= Quaternion.AngleAxis(speed , Vector3.up) ;
        visualEffect.SetFloat("SparnRate", Mathf.Lerp(0, maxPartikleSpawn, curendState));
    }

}
