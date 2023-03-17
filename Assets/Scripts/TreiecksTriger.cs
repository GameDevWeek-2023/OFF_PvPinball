using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreiecksTriger : MonoBehaviour
{

    public Animator goastAnim, normalAnim;
    public float normalForce = 1000f, speedmul = 0.3f;
    public float VelocityStop = 0.3f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Debug.Log(other.gameObject.layer);
            normalAnim.SetTrigger("Bump");
        }
        if (other.gameObject.layer == 8)
        {
            Debug.Log(other.gameObject.layer);
            goastAnim.SetTrigger("Bump");
        }

        var rig = other.GetComponent<Rigidbody>();
        rig.velocity *= VelocityStop;
        var dir = (other.transform.position - transform.position).normalized  -transform.right;
        dir *= 0.5f;
        var sp = rig.velocity.magnitude * speedmul;
        rig.AddForce(dir * (normalForce + sp),ForceMode.Impulse);
    }
}
