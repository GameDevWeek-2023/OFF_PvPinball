using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tEST : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = Vector3.zero;
    }

    private void FixedUpdate()
    {
        
        GetComponent<Rigidbody>().AddTorque(transform.right * 10);
    }
}
