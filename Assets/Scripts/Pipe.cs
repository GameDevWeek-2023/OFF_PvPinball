using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Pipe : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform exitPoint;
    public float travelTime;
    public float force;
    public float spawnForce;
    public float spawnDelay;

    public GameObject ball, ballLayerTwo;

    private bool canSpawn = true;
    

    private void OnCollisionEnter(Collision collision)
    {
        print("bla");
        if (collision.gameObject.GetComponent<Ball>() != null)
        {
            print("ball hit");
            StartCoroutine(TravelBall(collision.gameObject.GetComponent<Ball>().isLayerTwo));
            Destroy(collision.gameObject);
        }
    }

    IEnumerator TravelBall(bool isLayerTwo)
    {
        yield return new WaitForSeconds(travelTime);

        GameObject b;
        
        if (!isLayerTwo)
        {
            b = Instantiate(ball, exitPoint.transform.position, Quaternion.identity, UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects()[0].transform);
        }
        else
        {
            b = Instantiate(ballLayerTwo, exitPoint.transform.position, Quaternion.identity, UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects()[0].transform);
        }
        
        b.gameObject.GetComponent<Rigidbody>().AddForce(exitPoint.forward * force);
    }

    IEnumerator SpawnDelay(Rigidbody rig)
    {
        yield return new WaitForSeconds(spawnDelay);
        rig.isKinematic = false;
        rig.AddForce(spawnPoint.forward * force);
        canSpawn = true;
    }

    public void SpawnBall(int type)
    {
        canSpawn = false;
        if (type == 0)
        {
            GameObject b = Instantiate(ball, spawnPoint.transform.position, Quaternion.identity, UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects()[0].transform);
            Rigidbody rig = b.GetComponent<Rigidbody>();
            rig.isKinematic = true;
            StartCoroutine(SpawnDelay(rig));
        }
        else
        {
            GameObject b = Instantiate(ballLayerTwo, spawnPoint.transform.position, Quaternion.identity,UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects()[0].transform);
            Rigidbody rig = b.GetComponent<Rigidbody>();
            rig.isKinematic = true;
            StartCoroutine(SpawnDelay(rig));
        }
    }
    
    
}
