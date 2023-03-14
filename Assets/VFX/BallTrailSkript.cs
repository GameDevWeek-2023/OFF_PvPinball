using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrailSkript : MonoBehaviour
{
    Renderer ren;
    Rigidbody rig;
    public Vector3 force;
    int i = 0;
    public Material grasAuftelMaterial;
    public RenderTexture renderTarget, prevRenderTarget;
    // Start is called before the first frame update
    void Start()
    {
        ren = GetComponent<Renderer>();
        rig = GetComponentInParent<Rigidbody>();
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
    }
}
