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
    public float centerOffmaceOfset = 1;
    public float rotationStrengtch = 1;
    public float IdelTrigerTime = 20, idelTrigerTimer;
    public bool animatOnCurv = false;
    private float t = 0;
    public float animSpeed = 1;
    public AnimationCurve animationHeight;

    public Transform AnimEndPunkt;

    public Vector3 sollPos , istPos;
    public Quaternion sollQuat, isQuat;

    private void Awake()
    {
        base.Awake();
        rig = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        rig.centerOfMass = Vector3.zero - Vector3.up * centerOffmaceOfset;
        idelTrigerTimer = IdelTrigerTime;

        istPos = transform.position;
        isQuat = transform.rotation;

        sollPos = AnimEndPunkt.position;
        sollQuat = AnimEndPunkt.rotation;
    }

    private void Update()
    {
        IdelTriger();
        CheckvorBalls();

        if(lastAtack + atackRate < Time.time)
        {
            if(CurentTarget != null)
            {
            CalculateJump();
            AtackBall();
            }

        }

        if (CurentTarget != null)
        {
            
           // Debug.Log("angel" + angle);
            //rig.AddRelativeTorque(transform.up * angle * rotationStrengtch);
            
        }

        TravelOnCurv();
    }

    public void AnableCyberFrog()
    {
        animatOnCurv = true;
        Debug.Log("Enable");
    }
  
    public void TravelOnCurv()
    {
        if (animatOnCurv)
        {
            t += Time.deltaTime * animSpeed;
            transform.position = Vector3.Lerp(istPos, sollPos, t);
            transform.position += Vector3.up * animationHeight.Evaluate(t);
            transform.rotation = Quaternion.Lerp(isQuat,sollQuat, t);
            Debug.Log(t);
            if(t >= 1)
            {
                animatOnCurv = false;
                t = 0;
                EnableColider();
            }

        }
    }

    private void EnableColider()
    {
        foreach(var cild in GetComponents<Collider>())
        {
            cild.enabled = true;
        }
        rig.isKinematic = false;
    }
    private void IdelTriger()
    {
        idelTrigerTimer -= Time.deltaTime;
        if(idelTrigerTimer <= 0)
        {
            int ran = Random.Range(1, 3);
            anim.SetInteger("IdelTriger", ran);
            Debug.Log(ran);
            idelTrigerTimer = IdelTrigerTime;
        }
        else
            anim.SetInteger("IdelTriger", 0);
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
        
        if(zuÜberBrücken.magnitude >= AtensionRadius)
        {
            zuÜberBrücken = zuÜberBrücken.normalized * AtensionRadius;
        }

        jumpForce = zuÜberBrücken / atackSpeed ;
        jumpForce *= distanzToSpeedCurve.Evaluate((Vector3.Distance(transform.position, CurentTarget.transform.position) / AtensionRadius));
        jumpForce -= Physics.gravity / atackSpeed / 100;

        Debug.DrawRay(targetPos, transform.up, Color.red, 1f);
    }
    private void JumpForce()
    {
        rig.MoveRotation(Quaternion.LookRotation(CurentTarget.transform.position - transform.position, transform.up));
        rig.velocity = jumpForce;
            CurentTarget = null;
    }
    private void AtackBall()
    {
        // Animationen Und Delay myb

        if(Vector3.Distance(transform.position , CurentTarget.transform.position) >= AtensionRadius)
        {
            CurentTarget = null;
        }
        else
        {

        lastAtack = Time.time;
        anim.SetTrigger("Jump");
        CurentTarget = null;
        }
    }

}
