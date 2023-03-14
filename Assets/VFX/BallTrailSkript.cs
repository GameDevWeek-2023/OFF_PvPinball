using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrailSkript : MonoBehaviour
{
    [SerializeField]Camera cam;
    Renderer ren;
    Rigidbody rig;
    public Vector3 force;
    int i = 0;
    public Material grasAuftelMaterial;
    public RenderTexture renderTexture1, renderTexture2;
    public RenderTexture renderTarget, prevRenderTarget;
    // Start is called before the first frame update
    void Start()
    {
        ren = GetComponent<Renderer>();
        rig = GetComponent<Rigidbody>();

        renderTarget = cam.targetTexture;
    }
    private void FixedUpdate()
    {
        var input = Input.GetAxis("Horizontal");
        var input2 = Input.GetAxis("Vertical");
        rig.AddForce(Vector3.right * input);
        rig.AddForce(Vector3.forward * input2);
    }
    // Update is called once per frame
    void Update()
    {
        ren.material.SetVector("_velocity", rig.velocity);
        var vel = rig.velocity;
        vel.y = 0;
        Debug.DrawRay(transform.position, vel.normalized * 4);
    }
    private void LateUpdate()
    {
        //Copy content from renderTarget to prevRenderTarget
        // fade prevRenderTarget with current renderTarget;
        Graphics.Blit(renderTarget, prevRenderTarget, grasAuftelMaterial);
        Graphics.Blit(prevRenderTarget, renderTarget);
        
        /*

        if(i == 0)
        {
            ren.material.SetTexture("_BallTrail", renderTexture2);
            cam.targetTexture = renderTexture1;
            //grasAuftelMaterial.SetTexture("_MainTex", renderTexture1);
            Graphics.Blit(renderTexture2, renderTexture1,grasAuftelMaterial);
            i++;

        }



        else
        {
            ren.material.SetTexture("_BallTrail", renderTexture1);
            cam.targetTexture = renderTexture2;
            //grasAuftelMaterial.SetTexture("_MainTex", renderTexture2);
            Graphics.Blit(renderTexture1, renderTexture2,grasAuftelMaterial);
            i--;
        }
        */
    }
}
