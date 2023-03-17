using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BounsPlant : MonoBehaviour
{
    [SerializeField]
    float maxSpeed, minSpeed;

    public float stateAbfall, stateAufbau, maxPartikleSpawn;
    [SerializeField]
    float curendState, sollState;
    public float speedMultiplier = 1.5f;
    public float standertForce = 10f;
    [SerializeField]
    private VisualEffect visualEffect;
    Animator animator;
    private void Start()
    {
        visualEffect = GetComponent<VisualEffect>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        sollState = 1;
        var rigidbody = collision.collider.attachedRigidbody;
        rigidbody.AddForce((standertForce + rigidbody.velocity.magnitude * speedMultiplier) * (rigidbody.position - transform.position).normalized);
        FindObjectOfType<AudioManager>().Play("Bumper");
        animator.SetTrigger("Hit");
    }
    private void Update()
    {

        if (curendState != sollState)
        {
            if (curendState < sollState)
            {
                curendState += stateAufbau;
                curendState = Mathf.Clamp(curendState, 0f, 1f);
            }
            else
            {
                curendState -= stateAbfall;
                curendState = Mathf.Clamp(curendState, 0f, 1f);
            }
        }

        sollState = Mathf.Clamp01(sollState - stateAbfall);
        visualEffect.SetFloat("SparnRate", Mathf.Lerp(0, maxPartikleSpawn, curendState));
        Debug.Log(Mathf.Lerp(0, maxPartikleSpawn, curendState));
    }

}
