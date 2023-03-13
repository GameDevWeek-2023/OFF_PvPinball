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

    public Vector3 startValuesLeft;
    public Vector3 startValuesRight;
    
    private Coroutine rotationCoroutineLeft;
    private Coroutine rotationCoroutineRight;


    private void LateUpdate()
    {
        //flipperLeft.transform.position = startValuesLeft;
        //flipperRight.transform.position = startValuesRight;
        
        rigRight.centerOfMass = Vector3.zero;
        rigLeft.centerOfMass = Vector3.zero;

        Vector3 flipperLeftAngle = flipperLeft.eulerAngles;
        Vector3 flipperRightAngle = flipperRight.eulerAngles;
        
        /*float newRotLeft = Mathf.Clamp(flipperLeftAngle.y, 65, 110);
        flipperLeft.rotation = Quaternion.Euler(flipperLeftAngle.x, newRotLeft, flipperLeftAngle.z);
        
        float newRotRight = Mathf.Clamp(flipperRightAngle.y, 250, 295);
        flipperRight.rotation = Quaternion.Euler(flipperRightAngle.x, newRotRight, flipperRightAngle.z);*/


        if (flipperLeftAngle.y <= 65)
        {
            rigLeft.angularVelocity = Vector3.zero;
        }

        if (flipperLeftAngle.y >= 110)
        {
            rigLeft.angularVelocity = Vector3.zero;
        }
        
        if (flipperRightAngle.y >= 294)
        {
            rigRight.angularVelocity = Vector3.zero;
        }

        if (flipperRightAngle.y <= 250)
        {
            rigRight.angularVelocity = Vector3.zero;
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
    
    
    void OnLeftTrigger(InputValue value)
    {
        float val = value.Get<float>();
        
        if (val > 0)
        {
            /*if (rotationCoroutineLeft != null)
            {
                StopCoroutine(rotationCoroutineLeft);
            }
            rotationCoroutineLeft = StartCoroutine(RotateTrigger(-1, pivotPointLeft));*/

            if (flipperLeft.eulerAngles.y > 65)
            {
                rigLeft.AddTorque(Vector3.up * force * -1);
            }
        }
        
        if (val <= 0)
        {
            if (rotationCoroutineLeft != null)
            {
                StopCoroutine(rotationCoroutineLeft);
            }

            //pivotPointLeft.transform.eulerAngles = new Vector3(pivotPointLeft.transform.eulerAngles.x, 0,
                //pivotPointLeft.transform.eulerAngles.z);
        }
    }
    
    
    void OnRightTrigger(InputValue value)
    {
        float val = value.Get<float>();
        
        if (val > 0)
        {
            /*if (rotationCoroutineRight != null)
            {
                StopCoroutine(rotationCoroutineRight);
            }
            rotationCoroutineRight = StartCoroutine(RotateTrigger(1, pivotPointRight));*/

            if (flipperRight.eulerAngles.y < 265)
            {
                rigRight.AddTorque(Vector3.up * force);
            }
            
        }
        
        if (val <= 0)
        {
            if (rotationCoroutineRight != null)
            {
                StopCoroutine(rotationCoroutineRight);
            }
            //pivotPointRight.transform.eulerAngles = new Vector3(pivotPointRight.transform.eulerAngles.x, 0,
                //pivotPointRight.transform.eulerAngles.z);
        }
    }
}
