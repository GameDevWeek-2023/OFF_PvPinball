using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class FollowBezierCurve : MonoBehaviour
{
    public PathCreator pathCreator;
    [SerializeField] private Transform aaa;
    [SerializeField] private float speed;
    [SerializeField] private float endDistance;
    public EndOfPathInstruction end;
    private Vector3 endpoint;
    private float dstTraveled = 0f;
    private Rigidbody _rb;

    private bool arrived;
    

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        endpoint = aaa.position;
        transform.position = pathCreator.path.GetPointAtTime(0);
    }
    // Update is called once per frame
    void Update()
    {

        if (arrived)
        {
            enabled = false;
        }

        if (Vector3.Distance(transform.position, endpoint) > endDistance)
        {
            dstTraveled += Time.deltaTime * speed;
            transform.position = pathCreator.path.GetPointAtDistance(dstTraveled);
            transform.rotation = pathCreator.path.GetRotationAtDistance(dstTraveled);
        }
        else
        {
            _rb.AddForce(transform.forward ,ForceMode.Impulse);
            arrived = true;

        }
        
    }
}
