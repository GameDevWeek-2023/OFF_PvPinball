using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nephentes : MonoBehaviour
{
    public NephentesTrigger trigger;

    public Transform pivot;
    public float rotationAngle;
    public float rotateDuration = 1f;
    
    private Quaternion startRotation;
    private Quaternion endRotation;

    private bool isAtStart = true;
    private bool isRotating = false;

    private void Start()
    {
        startRotation = pivot.localRotation;
        endRotation = startRotation * Quaternion.AngleAxis(rotationAngle, Vector3.up);
        //StartCoroutine(RotateNephentes(true));
    }

    public void CaughtBall()
    {
        if (!isRotating)
        {
            isRotating = true;
            if (isAtStart)
            {
                StartCoroutine(RotateNephentes(true));
                isAtStart = false;
                
            }
            else
            {
                StartCoroutine(RotateNephentes(false));
                isAtStart = true;
            }
        }
    }
    
    public void ReleaseBall()
    {
        print("releasing ball");
        trigger.ExitBall();
    }
    
    IEnumerator RotateNephentes(bool direction)
    {
        float t = 0.0f;

        if (direction)
        {
            while ( t  < rotateDuration)
            {
                t += Time.deltaTime;
                print(t);
                pivot.localRotation = Quaternion.Lerp(startRotation, endRotation, t / rotateDuration);
                
                yield return null;
            }
        }
        else
        {
            while ( t  < rotateDuration)
            {
                t += Time.deltaTime;
                pivot.localRotation = Quaternion.Lerp(endRotation, startRotation, t / rotateDuration);
                yield return null;
            }
        }

        isRotating = false;
    }

    void OnEnter()
    {
        CaughtBall();
    }
}
