using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WallBounce : MonoBehaviour
{

    [SerializeField] Renderer GlowRenderer;
    Material GlowMaterial;
    private IngameHighscoreManager ihm;

    public GameObject Rotoren;
    [SerializeField]
    float speed,maxSpeed, minSpeed;

    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color minColor, maxColor, scoreColor;

    public float stateAbfall,stateAufbau , maxPartikleSpawn;
    [SerializeField]
    float curendState,sollState;
    public float speedMultiplier = 1.5f;
    public float standertForce = 10f;
    [SerializeField]
    private VisualEffect visualEffect;
    AudioManager audioManager;

    private void Start()
    {
        visualEffect = GetComponent<VisualEffect>();
        ihm = FindFirstObjectByType<IngameHighscoreManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
            var rigidbody = collision.collider.attachedRigidbody;
            var direction = (rigidbody.position - transform.position).normalized;

            ihm.Score(500, this.transform , -direction *rigidbody.velocity.magnitude,scoreColor);
            rigidbody.AddForce((standertForce + rigidbody.velocity.magnitude * speedMultiplier) * direction);
            sollState = 1;
            audioManager.Play("Bumper");

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
