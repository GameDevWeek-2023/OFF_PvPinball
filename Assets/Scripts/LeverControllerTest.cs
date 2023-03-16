using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeverControllerTest : MonoBehaviour
{
    public float rotateDuration;
    public float rotationAngle = 45;
    public float force = 10;

    public Transform flipperLeft, flipperLeft_L2;
    public Transform flipperRight, flipperRight_L2;

    public Rigidbody rigLeft, rigLeft_L2;
    public Rigidbody rigRight, rigRight_L2;  

    //public float flipperLeftMaxAngle, flipperLeftMinAngle;
    //public float flipperRightMaxAngle, flipperRightMinAngle;

    public float flipperRotationAngle;

    public bool controlHeld;
    
    

    private void Start()
    {
        rigRight.centerOfMass = Vector3.zero;
        rigLeft.centerOfMass = Vector3.zero;

        rigRight_L2.centerOfMass = Vector3.zero;
        rigLeft_L2.centerOfMass = Vector3.zero;
    }

    private void FixedUpdate()
    {
        Vector3 flipperLeftAngle = flipperLeft.localEulerAngles;
        Vector3 flipperLeftAngle_L2 = flipperLeft_L2.localEulerAngles;
        Vector3 flipperRightAngle = flipperRight.localEulerAngles;
        Vector3 flipperRightAngle_L2 = flipperRight_L2.localEulerAngles;

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
    }

    void OnLeftTrigger(InputValue value)
    {
        float val = value.Get<float>();
        Debug.Log("L1L");
        if (val > 0) 
        {
                rigLeft.AddTorque(rigLeft.transform.up * force * -1);
                FindObjectOfType<AudioManager>().Play("FlipperUp");
        }  
        else
        {
            rigLeft.angularVelocity = Vector3.zero;
            rigLeft.AddTorque(rigLeft.transform.up * force * 1);
        }
    }
    
    void OnLeftTriggerLayer2(InputValue value)
    {
        float val = value.Get<float>();
        Debug.Log("L2L");
        if (val > 0) 
        {
                rigLeft_L2.AddTorque(rigLeft_L2.transform.up * force * -1); 
                FindObjectOfType<AudioManager>().Play("FlipperUp");
        }
        else
        {
            rigLeft_L2.angularVelocity = Vector3.zero;
            rigLeft_L2.AddTorque(rigLeft_L2.transform.up * force * 1);
        }
    }
    
    
    void OnRightTrigger(InputValue value)
    {
        float val = value.Get<float>();
        Debug.Log("L1R");
        if (val > 0)
        {
                rigRight.AddTorque(rigRight.transform.up * force);
                FindObjectOfType<AudioManager>().Play("FlipperUp");
        }
        else
        {
            rigRight.angularVelocity = Vector3.zero;
            rigRight.AddTorque(rigRight.transform.up * -force);
            
        }
    }
    
    void OnRightTriggerLayer2(InputValue value)
    {
        float val = value.Get<float>();
        Debug.Log("L2R");
        if (val > 0)
            {
                rigRight_L2.AddTorque(rigRight_L2.transform.up * force);
                FindObjectOfType<AudioManager>().Play("FlipperUp");
            }
        else
        {
            rigRight_L2.angularVelocity = Vector3.zero;
            rigRight_L2.AddTorque(rigRight_L2.transform.up * -force);
        }
        
    } 
}
