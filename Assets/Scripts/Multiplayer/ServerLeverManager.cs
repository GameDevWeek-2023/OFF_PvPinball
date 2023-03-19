using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class ServerLeverManager : NetworkBehaviour
{
    public float force = 10;

    private Transform flipperLeft, flipperLeft_L2;
    private Transform flipperRight, flipperRight_L2;

    private Rigidbody rigLeft, rigLeft_L2;
    private Rigidbody rigRight, rigRight_L2;  
    
    public float flipperRotationAngle;

    public GameObject l1, l2, r1, r2;
    

    public override void OnStartServer()
    {
        base.OnStartServer();
        
        rigRight.centerOfMass = Vector3.zero;
        rigLeft.centerOfMass = Vector3.zero;

        rigRight_L2.centerOfMass = Vector3.zero;
        rigLeft_L2.centerOfMass = Vector3.zero;

        rigLeft = l1.GetComponent<Rigidbody>();
        rigLeft_L2 = l2.GetComponent<Rigidbody>();
        rigRight = r1.GetComponent<Rigidbody>();
        rigRight_L2 = r2.GetComponent<Rigidbody>();

        flipperLeft = l1.transform;
        flipperLeft_L2 = l2.transform;
        flipperRight = r1.transform;
        flipperRight_L2 = r2.transform;

        //NetworkServer.Spawn(l1);
        //NetworkServer.Spawn(l2);
        //NetworkServer.Spawn(r1);
        //NetworkServer.Spawn(r2);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        
        rigLeft = l1.GetComponent<Rigidbody>();
        rigLeft_L2 = l2.GetComponent<Rigidbody>();
        rigRight = r1.GetComponent<Rigidbody>();
        rigRight_L2 = r2.GetComponent<Rigidbody>();
        
        Destroy(rigLeft);
        Destroy(rigLeft_L2);
        Destroy(rigRight);
        Destroy(rigRight_L2);
    }
    
    
    private void FixedUpdate()
    {
        /*if (isServer)
        {
            if (Quaternion.Angle(Quaternion.identity, flipperRight.transform.localRotation) >= flipperRotationAngle)
            {
                rigRight.angularVelocity = Vector3.zero;
                rigRight.transform.localRotation = Quaternion.RotateTowards(Quaternion.identity, rigRight.transform.localRotation, flipperRotationAngle);
            }
            if (Quaternion.Angle(Quaternion.identity, flipperRight_L2.transform.localRotation) >= flipperRotationAngle)
            {
                rigRight_L2.angularVelocity = Vector3.zero;
                rigRight_L2.transform.localRotation = Quaternion.RotateTowards(Quaternion.identity, rigRight_L2.transform.localRotation, flipperRotationAngle);

            }
            if (Quaternion.Angle(Quaternion.identity, flipperLeft.transform.localRotation) >= flipperRotationAngle)
            {
                rigLeft.angularVelocity = Vector3.zero;
                rigLeft.transform.localRotation = Quaternion.RotateTowards(Quaternion.identity, rigLeft.transform.localRotation, flipperRotationAngle);
            }
            if (Quaternion.Angle(Quaternion.identity, flipperLeft_L2.transform.localRotation) >= flipperRotationAngle)
            {
                rigLeft_L2.angularVelocity = Vector3.zero;
                rigLeft_L2.transform.localRotation = Quaternion.RotateTowards(Quaternion.identity, rigLeft_L2.transform.localRotation, flipperRotationAngle);
            }
        }*/
    }

    [Server]
    public void LeftTrigger(float val)
    {
        if (val > 0) 
        {
            rigLeft.AddTorque(rigLeft.transform.up * force * -1);
        }  
        else
        {
            rigLeft.angularVelocity = Vector3.zero;
            rigLeft.AddTorque(rigLeft.transform.up * force * 1);
        }
    }
    
    [Server]
    public void LeftTriggerL2(float val)
    {
        if (val > 0) 
        {
            rigLeft_L2.AddTorque(rigLeft_L2.transform.up * force * -1);
        }
        else
        {
            rigLeft_L2.angularVelocity = Vector3.zero;
            rigLeft_L2.AddTorque(rigLeft_L2.transform.up * force * 1);
        }
    }

    [Server]
    public void RightTrigger(float val)
    {
        if (val > 0)
        {
            rigRight.AddTorque(rigRight.transform.up * force);
        }
        else
        {
            rigRight.angularVelocity = Vector3.zero;
            rigRight.AddTorque(rigRight.transform.up * -force);
        }
    }

    [Server]
    public void RightTriggerL2(float val)
    {
        if (val > 0)
        {
            rigRight_L2.AddTorque(rigRight_L2.transform.up * force);
        }
        else
        {
            rigRight_L2.angularVelocity = Vector3.zero;
            rigRight_L2.AddTorque(rigRight_L2.transform.up * -force);
        }
    }
    
}
