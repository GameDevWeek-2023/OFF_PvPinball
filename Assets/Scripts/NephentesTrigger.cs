using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NephentesTrigger : MonoBehaviour
{

    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color scoreColor;

    public Nephentes nephentes;
    public bool hasBall;
    public Animator animator;
    private IngameHighscoreManager ihm;
    AudioManager audioManager;
    public Transform exitPoint;
    public GameObject ballPrefab;

    public float exitForce;

    public float resetTime = 1;
    
    private static readonly int hashBallEntered = Animator.StringToHash("BallEntered");

    private void Awake()
    {
        ihm = FindFirstObjectByType<IngameHighscoreManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

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
        ihm.Score(1200, this.transform , transform.up * 20, scoreColor);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(resetTime);
        hasBall = false;
    }
}
