using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class FollowBezierCurve : MonoBehaviour
{
    public PathCreator pathCreator;
    [SerializeField] private float speed;
    private float dstTraveled = 0f;
    

    void Start()
    {
        transform.position = pathCreator.path.GetPointAtTime(0);
    }
    // Update is called once per frame
    void Update()
    {
        dstTraveled += Time.deltaTime * speed;
        transform.position = pathCreator.path.GetPointAtDistance(dstTraveled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(dstTraveled);
    }
}
