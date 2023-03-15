using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Katapult : MonoBehaviour
{
   public Rigidbody rig;
   public Transform pivot;
   public float force;
   public float shootForce = 10;

   public float rotationAngle;
   public Vector3 startValues;
   
   public float minAngle;
   public float maxAngle;

   public float z;

   public Transform ballPoint;
   public GameObject ball, ballLayerTwo;

   private GameObject currentBall;

   public float fireTime = 2;
   private void Start()
   {
      minAngle = pivot.eulerAngles.z;
      maxAngle = minAngle - rotationAngle;
      rig.centerOfMass = Vector3.zero;
      currentBall = null;

      StartCoroutine(FireRoutine());
   }

   private void LateUpdate()
   {
      //pivot.transform.position = startValues;
      
      Vector3 angle = pivot.eulerAngles;
      z = pivot.localRotation.z * 180;
      
      if (z <= maxAngle)
      {
         if (currentBall != null)
         {
            Destroy(currentBall);
            currentBall = null;
            
            var b = Instantiate(ball, ballPoint.transform.position, Quaternion.identity);
            b.GetComponent<Rigidbody>().AddForce(ballPoint.forward * shootForce);
         }
         
         rig.AddTorque(pivot.forward * 10);
         //rig.angularVelocity = Vector3.zero;
         //pivot.eulerAngles = new Vector3(angle.x, angle.y, maxAngle);
      }
      
      if (z >= minAngle)
      {
         rig.angularVelocity = Vector3.zero;
         pivot.eulerAngles = new Vector3(angle.x,angle.y, minAngle);
      }
   }

   
   void OnFire(InputValue value)
   {
      
      float input = value.Get<float>();

      if (input > 0)
      {
         Fire();
         
      }
   }

   public void Fire()
   {
      if (currentBall == null)
      {
         LoadBall();
      }
      z = pivot.localRotation.z * 180;

      if (z >= -1)
      {
         rig.AddTorque(pivot.forward * force* -3);
      }
   }

   IEnumerator FireRoutine()
   {
      while (true)
      {
         Fire();
         yield return new WaitForSeconds(fireTime);
      }
   }

   public void LoadBall()
   {
      print("loading ball");
      currentBall = Instantiate(ball, ballPoint.transform.position, Quaternion.identity, ballPoint);
      currentBall.GetComponent<Rigidbody>().isKinematic = true;
   }
}
