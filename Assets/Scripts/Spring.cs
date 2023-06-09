using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public GameObject Barrier;

    public List<GameObject> balls = new List<GameObject>();
    public float force = 10;

    public float barrierTime = 0.75f;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Ball>() != null)
        {
            balls.Add(other.gameObject);
            StartCoroutine(SpringRoutine());
        }
    }

    IEnumerator SpringRoutine()
    {
        yield return new WaitForSeconds(0.25f);
        foreach (GameObject ball in balls)
        {
            if (ball != null)
            {
                ball.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
            }
        }
        
        yield return new WaitForSeconds(barrierTime);
        Barrier.SetActive(true);
    }
}
