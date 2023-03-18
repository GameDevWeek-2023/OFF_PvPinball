
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform teletarget;
    [SerializeField] private float force;
    [SerializeField] private VisualEffect vfx;
    [SerializeField] private float spawnOffset = 1f;
    [SerializeField] private float rate = 1f;
    private Vector3 forward;

    void Start(){
        
        rate = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        forward = 
        vfx.SetFloat("SpawnRate", rate);
        rate -= 0.1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        forward = teletarget.forward;
        rate = 5f;
        Rigidbody rb = other.attachedRigidbody;
        other.transform.position = teletarget.position + forward * spawnOffset;
        rb.velocity = rb.velocity.magnitude * forward * force;

    }
}
