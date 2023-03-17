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
    private void Awake()
    {
        splitter = GetComponentsInChildren<Rigidbody>();
        splitterRen = GetComponentsInChildren<Renderer>();
        

    }
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody balrig = other.attachedRigidbody;

        for (int i = 0; i < splitter.Length; i++)
        {
            var dir = splitter[i].transform.position - balrig.transform.position;
            var distanz = dir.magnitude;
            var forcestreng = Mathf.Clamp01(maxForceDistanz - distanz);



            splitter[i].isKinematic = false;
            splitter[i].AddForce(dir.normalized * forcestreng * speedMultyplier * 5 * (standertForce + balrig.velocity.magnitude * speedMultyplier), ForceMode.Impulse);


            splitterRen[i].enabled = true;
        }

        DomeRenderer.enabled = false;
        DomeCol.enabled = false;



    }


    private void OnCollisionEnter(Collision collision)
    {
        HitCount -= 1;
        if(HitCount == 0)
        {
            GetComponent<Collider>().isTrigger = true;
        }

    }


}
