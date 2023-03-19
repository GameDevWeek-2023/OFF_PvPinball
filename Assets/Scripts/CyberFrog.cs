using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberFrog : ScorebelObjeckt
{

    Animator anim;
    public LayerMask ballLayer;
    public float AtensionRadius = 5;
    public float HitPoints = 5;
    public bool isAktiv = false;
    public float atackRate;
    private GameObject CurentTarget;
    private Vector3 jumpForce;
    public float atackSpeed = 0.2f;
    public Rigidbody rig;
    public float lastAtack;
    public AnimationCurve distanzToSpeedCurve;

    private void Awake()
    {
        base.Awake();
        rig = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckvorBalls();

        if(lastAtack + atackRate < Time.time)
        {
            if(CurentTarget != null)
            {
            CalculateJump();
            AtackBall();
            }

        }

    }

    private void CheckvorBalls()
    {
        var balls = Physics.OverlapSphere(transform.position, AtensionRadius, ballLayer);
        float distanz = 0 , shortestDistanz = 1000;

        foreach (var bal in balls)
        {
            distanz = (bal.transform.position - transform.position).magnitude;
            if(distanz < shortestDistanz)
            {
                CurentTarget = bal.gameObject;
            }
        }
    }

    private void CalculateJump()
    {
        // ungefähr 0.2s bis zur Colision
        Vector3 targetPos = CurentTarget.transform.position;
        targetPos += CurentTarget.GetComponent<Rigidbody>().velocity * atackSpeed;

        Vector3 zuÜberBrücken = targetPos - transform.position;

        jumpForce = zuÜberBrücken / atackSpeed;
        jumpForce += Physics.gravity * atackSpeed;
        jumpForce *= distanzToSpeedCurve.Evaluate((Vector3.Distance(transform.position, CurentTarget.transform.position) / AtensionRadius));

        Debug.DrawRay(targetPos, transform.up, Color.red, 1f);
    }
    private void AtackBall()
    {
        // Animationen Und Delay myb
        if (Vector3.Distance(transform.position, CurentTarget.transform.position) <= AtensionRadius)
        {

            lastAtack = Time.time;
            rig.velocity = jumpForce;
            CurentTarget = null;
        }
        else
        {
            CurentTarget = null;
        }    
                
    }

}
