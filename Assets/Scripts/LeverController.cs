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

    public Transform flipperLeft;
    public Transform flipperRight;

    public Rigidbody rigLeft;
    public Rigidbody rigRight;
    
    private Coroutine rotationCoroutineLeft;
    private Coroutine rotationCoroutineRight;

    public float flipperLeftMaxAngle, flipperLeftMinAngle;
    public float flipperRightMaxAngle, flipperRightMinAngle;

    public float flipperRotationAngle;

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

        Vector3 flipperLeftAngle = flipperLeft.eulerAngles;
        Vector3 flipperRightAngle = flipperRight.eulerAngles;
        
        if (flipperLeftAngle.y <= flipperLeftMaxAngle)
        {
            rigLeft.angularVelocity = Vector3.zero;
            flipperLeft.eulerAngles = new Vector3(flipperLeftAngle.x, flipperLeftMaxAngle, flipperLeftAngle.z);
        }

        if (flipperLeftAngle.y >= flipperLeftMinAngle)
        {
            rigLeft.angularVelocity = Vector3.zero;
            flipperLeft.eulerAngles = new Vector3(flipperLeftAngle.x, flipperLeftMinAngle, flipperLeftAngle.z);
        }
        
        if (flipperRightAngle.y >= flipperRightMaxAngle)
        {
            rigRight.angularVelocity = Vector3.zero;
            flipperRight.eulerAngles = new Vector3(flipperRightAngle.x, flipperRightMaxAngle, flipperRightAngle.z);
        }

        if (flipperRightAngle.y <= flipperRightMinAngle)
        {
            rigRight.angularVelocity = Vector3.zero;
            flipperRight.eulerAngles = new Vector3(flipperRightAngle.x, flipperRightMinAngle, flipperRightAngle.z);
        }
        
    }
    
    void OnLeftTrigger(InputValue value)
    {
        float val = value.Get<float>();
        
        if (val > 0)
        {
            if (flipperLeft.eulerAngles.y > 65)
            {
                rigLeft.AddTorque(Vector3.up * force * -1);
            }
        }
        
        if (val <= 0)
        {
            rigLeft.angularVelocity = Vector3.zero;
            rigLeft.AddTorque(Vector3.up * force);
        }
    }
    
    
    void OnRightTrigger(InputValue value)
    {
        float val = value.Get<float>();
        
        if (val > 0)
        {
            if (flipperRight.eulerAngles.y < 265)
            {
                rigRight.AddTorque(Vector3.up * force);
            }
            
        }
        
        if (val <= 0)
        {
            rigRight.angularVelocity = Vector3.zero;
            rigRight.AddTorque(Vector3.up * force * -1);
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
