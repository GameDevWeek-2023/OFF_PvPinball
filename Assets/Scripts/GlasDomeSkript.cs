using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlasDomeSkript : MonoBehaviour
{
    Rigidbody[] splitter;
    Renderer[] splitterRen;
    [SerializeField]
    Renderer DomeRenderer;
    [SerializeField]
    Collider DomeCol;
    public float maxForceDistanz = 1;
    public float speedMultyplier = 0.5f;
    public float standertForce = 5;
    public float HitCount = 3;
    private IngameHighscoreManager ihm;
    AudioManager audioManager;
    private bool destroit = false , animate = false;
    public float resetTime = 10 , timeToReset = 0;

    List<Vector3> startPos, bodenPos;
    List<Quaternion> startQuat,bodenQuat;

    public AnimationCurve anim;

    [ColorUsageAttribute(true, true, 1f, 8f, 0.125f, 3f)]
    public Color scoreColor;

    private void Awake()
    {
        startPos = new List<Vector3>();
        bodenPos = new List<Vector3>();

        startQuat = new List<Quaternion>();
        bodenQuat = new List<Quaternion>();


        splitter = GetComponentsInChildren<Rigidbody>();
        splitterRen = GetComponentsInChildren<Renderer>();
            ihm = FindFirstObjectByType<IngameHighscoreManager>();
            audioManager = FindObjectOfType<AudioManager>();
        foreach(Transform child in transform)
        {
            startPos.Add(child.position);
            startQuat.Add(child.rotation);
        }
       


    }
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody balrig = other.attachedRigidbody;

        for (int i = 0; i < splitter.Length; i++)
        {
            var dir = splitter[i].transform.position - balrig.transform.position;
            var distanz = dir.magnitude;
            var forcestreng = Mathf.Clamp01(maxForceDistanz - distanz);
            var spped = dir.normalized * forcestreng * speedMultyplier * 5 * (standertForce + balrig.velocity.magnitude * speedMultyplier);

            ihm.Score(50, this.transform, spped * 3, scoreColor);
            splitter[i].isKinematic = false;
            splitter[i].AddForce(spped, ForceMode.Impulse);


            splitterRen[i].enabled = true;
        }

        DomeRenderer.enabled = false;
        DomeCol.enabled = false;
        destroit = true;
        timeToReset = 0;



    }


    private void OnCollisionEnter(Collision collision)
    {
        HitCount -= 1;
        if(HitCount == 0)
        {
            GetComponent<Collider>().isTrigger = true;
        }
            ihm.Score(100 * (3 - (int) HitCount), this.transform, collision.rigidbody.velocity, scoreColor);

    }

    private void Update()
    {
        if(destroit)
        {
            timeToReset += Time.deltaTime;
            if(timeToReset >= resetTime)
            {
                int i = 0;
                foreach (Transform child in transform)
                {
                    bodenPos.Add(child.position);
                    bodenQuat.Add(child.rotation);
                    splitter[i++].isKinematic = true;
                }
                animate = true;
            }
        }


    }

}
