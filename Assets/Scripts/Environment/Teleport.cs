
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Teleport : MonoBehaviour
{
    [SerializeField] public Transform teletarget;
    [SerializeField] public Transform ghostTarget;
    [SerializeField] public float force;
    
    [SerializeField] private  VisualEffect[] vfxqueue;
    [SerializeField] private VisualEffect[] vfxqueue2;
    [SerializeField] public float spawnOffset = 1f;
    [SerializeField] private float spawnrate = 1f;
    [SerializeField] public GameObject ghostball;
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

    // Plays the Ghost Ball VFX
    public void playvfx()
    {
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

    public void spawnghost()
    {
        GameObject b = Instantiate(ghostball, ghostTarget.position + ghostTarget.forward * spawnOffset ,Quaternion.identity,null);
        Rigidbody rig = b.GetComponent<Rigidbody>();
        rig.AddForce(ghostTarget.forward * (force),ForceMode.Impulse);
        FindObjectOfType<AudioManager>().Play("BallStart");
        playvfx();
        
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
