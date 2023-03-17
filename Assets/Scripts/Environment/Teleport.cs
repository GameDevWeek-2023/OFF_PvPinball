
using System;
using Unity.VisualScripting;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform teletarget;
    [SerializeField] private float force;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = teletarget.position + teletarget.forward;
        other.attachedRigidbody.AddForce(teletarget.forward * force,ForceMode.Impulse);
        
    }
}
