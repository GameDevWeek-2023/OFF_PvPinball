using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NephentesTrigger : MonoBehaviour
{
    public Nephentes nephentes;
    public bool hasBall;
    public Animator animator;

    public Transform exitPoint;
    public GameObject ballPrefab;
    public float exitForce;

    public float resetTime = 1;
    
    private static readonly int hashBallEntered = Animator.StringToHash("BallEntered");
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Ball>() != null && !hasBall)
        {
            hasBall = true;
            Destroy(other.gameObject);
            animator.SetTrigger(hashBallEntered);
            //nephentes.CaughtBall();
        }
    }

    public void ExitBall()
    {
        GameObject ball = Instantiate(ballPrefab, exitPoint.position, Quaternion.identity);
        ball.GetComponent<Rigidbody>().AddForce(exitPoint.forward * exitForce, ForceMode.Impulse);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(resetTime);
        hasBall = false;
    }
}
