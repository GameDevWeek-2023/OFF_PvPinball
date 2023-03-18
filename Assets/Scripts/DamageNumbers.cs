using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumbers : MonoBehaviour
{
    // Start is called before the first frame update
    public float LiveTeim, CurendTime;
    public float ofsetY;
    public AnimationCurve scaleCureveX , scaleCureveY;
    public float Speed;
    public float forceScale = 0.05f;
    private Vector3 force;
    TextMeshPro text;
    private Camera cam;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
        cam = FindFirstObjectByType<Camera>();
    }


    public void Aktivate(float number,Transform pos,Vector3 force,Color color)
    {
        Debug.Log("Aktivate");
        this.gameObject.SetActive(true);
        transform.position = pos.position + Vector3.up * ofsetY ;
        text.text = number.ToString();
        text.faceColor = color;
        CurendTime = 0;
        force.y = 0;
        this.force = force * forceScale;
    }

    private void Update()
    {
        CurendTime += Time.deltaTime;
        transform.localScale = new Vector3(scaleCureveX.Evaluate(CurendTime / LiveTeim) , scaleCureveY.Evaluate(CurendTime / LiveTeim), 1);
        transform.position +=  (force + Vector3.up) * Speed * Time.deltaTime;
        transform.forward = transform.position - cam.transform.position;

        if(CurendTime >= LiveTeim)
        {
            this.gameObject.SetActive(false);
        }

    }

}
