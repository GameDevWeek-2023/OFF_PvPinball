using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeverController : MonoBehaviour
{
    public float rotateDuration;
    public float rotationAngle = 45;
    public float force = 10;

    public Transform flipperLeft, flipperLeft_L2;
    public Transform flipperRight, flipperRight_L2;

    public Rigidbody rigLeft, rigLeft_L2;
    public Rigidbody rigRight, rigRight_L2;
    
    private Coroutine rotationCoroutineLeft;
    private Coroutine rotationCoroutineRight;

    public float flipperLeftMaxAngle, flipperLeftMinAngle;
    public float flipperRightMaxAngle, flipperRightMinAngle;

    public float flipperRotationAngle;

    public bool controlHeld;
    
    

    private void Start()
    {
        flipperLeftMinAngle = flipperLeft.eulerAngles.y;
        flipperRightMinAngle = flipperRight.eulerAngles.y;

        flipperLeftMaxAngle = flipperLeftMinAngle - flipperRotationAngle;
        flipperRightMaxAngle = flipperRightMinAngle + flipperRotationAngle;
    }

    private void LateUpdate()
    {
        //flipperLeft.transform.position = startValuesLeft;
        //flipperRight.transform.position = startValuesRight;
        
        rigRight.centerOfMass = Vector3.zero;
        rigLeft.centerOfMass = Vector3.zero;
        
        rigRight_L2.centerOfMass = Vector3.zero;
        rigLeft_L2.centerOfMass = Vector3.zero;

        Vector3 flipperLeftAngle = flipperLeft.eulerAngles;
        Vector3 flipperLeftAngle_L2 = flipperLeft_L2.eulerAngles;
        Vector3 flipperRightAngle = flipperRight.eulerAngles;
        Vector3 flipperRightAngle_L2 = flipperRight_L2.eulerAngles;
        
        if (flipperLeftAngle.y <= flipperLeftMaxAngle)
        {
            rigLeft.angularVelocity = Vector3.zero;
            flipperLeft.eulerAngles = new Vector3(flipperLeftAngle.x, flipperLeftMaxAngle, flipperLeftAngle.z);
        }
        
        if (flipperLeftAngle_L2.y <= flipperLeftMaxAngle)
        {
            rigLeft_L2.angularVelocity = Vector3.zero;
            flipperLeft_L2.eulerAngles = new Vector3(flipperLeftAngle_L2.x, flipperLeftMaxAngle, flipperLeftAngle_L2.z);
        }

        if (flipperLeftAngle.y >= flipperLeftMinAngle)
        {
            rigLeft.angularVelocity = Vector3.zero;
            flipperLeft.eulerAngles = new Vector3(flipperLeftAngle.x, flipperLeftMinAngle, flipperLeftAngle.z);
        }
        
        if (flipperLeftAngle_L2.y >= flipperLeftMinAngle)
        {
            rigLeft_L2.angularVelocity = Vector3.zero;
            flipperLeft_L2.eulerAngles = new Vector3(flipperLeftAngle_L2.x, flipperLeftMinAngle, flipperLeftAngle_L2.z);
        }
        
        if (flipperRightAngle.y >= flipperRightMaxAngle)
        {
            rigRight.angularVelocity = Vector3.zero;
            flipperRight.eulerAngles = new Vector3(flipperRightAngle.x, flipperRightMaxAngle, flipperRightAngle.z);
        }
        
        if (flipperRightAngle_L2.y >= flipperRightMaxAngle)
        {
            rigRight_L2.angularVelocity = Vector3.zero;
            flipperRight_L2.eulerAngles = new Vector3(flipperRightAngle_L2.x, flipperRightMaxAngle, flipperRightAngle_L2.z);
        }

        if (flipperRightAngle.y <= flipperRightMinAngle)
        {
            rigRight.angularVelocity = Vector3.zero;
            flipperRight.eulerAngles = new Vector3(flipperRightAngle.x, flipperRightMinAngle, flipperRightAngle.z);
        }
        
        if (flipperRightAngle_L2.y <= flipperRightMinAngle)
        {
            rigRight_L2.angularVelocity = Vector3.zero;
            flipperRight_L2.eulerAngles = new Vector3(flipperRightAngle_L2.x, flipperRightMinAngle, flipperRightAngle_L2.z);
        }
        
    }
    
    void OnLeftTrigger(InputValue value)
    {
        float val = value.Get<float>();

        
        if (val > 0) 
        {
            if (flipperLeft.eulerAngles.y > flipperLeftMaxAngle)
            {
                rigLeft.AddTorque(Vector3.up * force * -1);
                FindObjectOfType<AudioManager>().Play("FlipperUp");
                    
            }
        }
        
        else
        {
            rigLeft.angularVelocity = Vector3.zero;
            rigLeft.AddTorque(Vector3.up * force);
            FindObjectOfType<AudioManager>().Play("FlipperDown");
        }
    }
    
    void OnLeftTriggerLayer2(InputValue value)
    {
        float val = value.Get<float>();
        
        if (val > 0) 
        {
            if (flipperLeft.eulerAngles.y > flipperLeftMaxAngle)
            {
                rigLeft_L2.AddTorque(Vector3.up * force * -1); 
                FindObjectOfType<AudioManager>().Play("FlipperUp");
            }
        }
        
        else
        {
            rigLeft_L2.angularVelocity = Vector3.zero;
            rigLeft_L2.AddTorque(Vector3.up * force);
            FindObjectOfType<AudioManager>().Play("FlipperDown");
        }
    }
    
    
    void OnRightTrigger(InputValue value)
    {
        float val = value.Get<float>();
        
        if (val > 0)
        {
            if (flipperRight.eulerAngles.y < flipperRightMaxAngle)
            {
                rigRight.AddTorque(Vector3.up * force);
                FindObjectOfType<AudioManager>().Play("FlipperUp");
            }
        }

        else
        {
            rigRight.angularVelocity = Vector3.zero;
            rigRight.AddTorque(Vector3.up * force * -1);
            FindObjectOfType<AudioManager>().Play("FlipperDown");
        }
    }
    
    void OnRightTriggerLayer2(InputValue value)
    {
        float val = value.Get<float>();

        if (val > 0)
        {
            if (flipperRight.eulerAngles.y < flipperRightMaxAngle)
            {
                rigRight_L2.AddTorque(Vector3.up * force);
                FindObjectOfType<AudioManager>().Play("FlipperUp");
            }
        }

        else
        {
            rigRight_L2.angularVelocity = Vector3.zero;
            rigRight_L2.AddTorque(Vector3.up * force * -1);
            FindObjectOfType<AudioManager>().Play("FlipperDown");
        }
    }
    
    // direction -1 for left trigger, 1 for right trigger
    
    IEnumerator RotateTrigger(int direction, Transform pivot)
    {
        float startRotation = pivot.transform.eulerAngles.y;
        float endRotation = direction * rotationAngle;
        
        float t = 0.0f;

        float difference = Mathf.Abs(endRotation) - startRotation;

        float percent = difference / rotationAngle;
        rotateDuration *= percent;

        while ( t  < rotateDuration)
        {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / rotateDuration);// % rotationAngle;
            pivot.transform.eulerAngles = new Vector3(pivot.transform.eulerAngles.x, yRotation, 
                pivot.transform.eulerAngles.z);
            yield return null;
        }
    }

}
