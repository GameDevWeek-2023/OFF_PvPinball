
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform teletarget;
    [SerializeField] private Transform ghostTarget;
    [SerializeField] private float force;
    
    [SerializeField] private  VisualEffect[] vfxqueue;
    [SerializeField] private VisualEffect[] vfxqueue2;
    [SerializeField] private float spawnOffset = 1f;
    [SerializeField] private float spawnrate = 1f;
    private float startrate;

    private Vector3 forward;
    private int len;
    private int index = 0;

    void Start()
    {
        len = vfxqueue.Length;

    }
    // Update is called once per frame
    void Update()
    {
    }
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.GameObject().layer != LayerMask.NameToLayer("BallLayer2"))
            {
                forward = teletarget.forward;
                Rigidbody rb = other.attachedRigidbody;
                other.transform.position = teletarget.position + forward * spawnOffset;
                rb.velocity = rb.velocity.magnitude * forward * force;
                if (index < len - 1)
                {
                    vfxqueue2[index].Reinit();
                    index++;
                }
                else
                {
                    index = 0;
                    vfxqueue2[index].Reinit();

                }
            }
            else
            {
                forward = ghostTarget.forward;
                Rigidbody rb = other.attachedRigidbody;
                other.transform.position = ghostTarget.position + forward * spawnOffset;
                rb.velocity = rb.velocity.magnitude * forward * force;
                if (index < len - 1)
                {
                    vfxqueue[index].Reinit();
                    index++;
                }
                else
                {
                    index = 0;
                    vfxqueue[index].Reinit();

                }
            }


        }
}
