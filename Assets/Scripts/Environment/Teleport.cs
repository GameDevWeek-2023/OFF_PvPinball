
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform teletarget;
    [SerializeField] private float force;
    [SerializeField] private VisualEffect vfx;

    float rate = 0;

    // Update is called once per frame
    void Update()
    {

        vfx.SetFloat("SpawnRate", rate);
        rate -= 0.1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = teletarget.position + teletarget.forward;
        other.attachedRigidbody.AddForce(teletarget.forward * force,ForceMode.Impulse);

        rate = 3;

    }
}
