using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClappenSkript : ScorebelObjeckt
{
    Renderer renderer;
    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color emisionColor;
    bool hit = false;
    private Quaternion startRotation;
    float rotationSpeed = 0.002f;
    Quaternion sollRot;
    [SerializeField]
    ClappenSkript[] otherClaps;
    float rotation = 1;
    

    private void Awake()
    {
        base.Awake();
        renderer = GetComponent<Renderer>();
        startRotation = transform.localRotation;
        sollRot = startRotation * Quaternion.AngleAxis(90, Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<Collider>().enabled = false;
        renderer.material.SetColor("_EmissionColor", emisionColor);
        Score(transform);
        hit = true;
    }
    public void Reset()
    {
        hit = false;

    }
    private void Update()
    {
            if(hit && rotation >= 0)
            {
                rotation -= rotationSpeed;
                transform.localRotation = Quaternion.Lerp(sollRot, startRotation, rotation);
                if(rotation <= 0)
                {
                    if (otherClaps[0].hit && otherClaps[1].hit)
                    {
                    Reset();
                    otherClaps[0].Reset();
                    otherClaps[1].Reset();

                    }
                }
            }
            else if(rotation <= 1)
            {
                transform.localRotation = Quaternion.Lerp(sollRot, startRotation, rotation);
                rotation += rotationSpeed;
                if(rotation >= 1)
            {
                GetComponent<Collider>().enabled = true;
            }

            }
        rotation = Mathf.Clamp01(rotation);
            
    }


}
