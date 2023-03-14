using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    
    public Transform exitPoint;
    public float travelTime;
    public float exitForce;

    public GameObject ball, ballLayerTwo;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Ball>() != null)
        {
            StartCoroutine(TravelBall(other.gameObject.GetComponent<Ball>().isLayerTwo));
            Destroy(other.gameObject);
        }
    }

    IEnumerator TravelBall(bool isLayerTwo)
    {
        yield return new WaitForSeconds(travelTime);

        GameObject b;
        
        if (!isLayerTwo)
        {
            b = Instantiate(ball, exitPoint);
        }
        else
        {
            b = Instantiate(ballLayerTwo, exitPoint);
        }
        
        b.gameObject.GetComponent<Rigidbody>().AddForce(exitPoint.forward * exitForce);
    }
}
