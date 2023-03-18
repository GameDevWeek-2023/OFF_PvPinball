using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Nephentes : MonoBehaviour
{
    public NephentesTrigger trigger;

    public Transform pivot;
    public float rotationAngle;
    public float rotateDuration = 1f;
    public float t , speed , s; 


    private Quaternion startRotation;
    private Quaternion endRotation;

    private bool isAtStart = true;
    private bool isRotating = false;
    public VisualEffect visualEffect;


    private void Start()
    {
        if (pivot != null)
        {
            startRotation = pivot.localRotation;
            endRotation = startRotation * Quaternion.AngleAxis(rotationAngle, Vector3.up);
        }
        //StartCoroutine(RotateNephentes(true));
    }
    private void Update()
    {
        if(t > 0)
        {
            t -= Time.deltaTime * speed;
            visualEffect.SetFloat("SparnRate", t);
        }
    }

    public void CaughtBall()
    {
        visualEffect.SetFloat("SparnRate", 0);
        if (!isRotating)
        {
            isRotating = true;
            if (isAtStart)
            {
                if (pivot != null)
                {
                    StartCoroutine(RotateNephentes(true));
                    isAtStart = false;
                }
            }
            else
            {
                if (pivot != null)
                {
                    StartCoroutine(RotateNephentes(false));
                    isAtStart = true;
                }
            }
        }
    }
    public void StartPartikle()
    {
        visualEffect.SetFloat("SparnRate", 1);
        t = 1;
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
