using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotAroundAxis : MonoBehaviour
{
    [SerializeField] private Vector3 rotAxis = new Vector3(0,0,1);
    [SerializeField] private float speed = 90f;

    private Vector3 rotVec;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {   rotVec = rotAxis.normalized * speed;
        transform.Rotate(rotVec * Time.deltaTime);
    }

    
}
