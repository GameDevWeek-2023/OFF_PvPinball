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

    public List<int> ballQueue = new List<int>();

    private bool canSpawn = true;

    public int startBalls;
    public int startGhostBalls;

    private bool gameStarted = false;

    private Coroutine spawnRoutine;
    public bool canShoot;

    private void Start()
    {
        LoadBalls();
    }

    public void LoadBalls()
    {
        for (int i = 0; i < startBalls; i++)
        {
            ballQueue.Add(0);
        }
        
        for (int i = 0; i < startGhostBalls; i++)
        {
            ballQueue.Add(1);
        }
    }
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
        canSpawn = false;
        yield return new WaitForSeconds(spawnDelay);
        rig.isKinematic = false;
        
        canSpawn = true;
    }
    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (ballQueue.Count > 0)
            {
                if (ballQueue[0] == 0)
                {
                    if (canShoot)
                    {
                        GameObject b = Instantiate(ball, spawnPoint.transform.position, Quaternion.identity, UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects()[0].transform);
                        Rigidbody rig = b.GetComponent<Rigidbody>();
                        rig.AddForce(spawnPoint.forward * force);
                        ballQueue.RemoveAt(0);
                        FindObjectOfType<AudioManager>().Play("BallStart");
                        //FindObjectOfType<AudioManager>().Play("Launch");
                    }
                    
                }
                else
                {
                    if (canShoot)
                    {
                        GameObject b = Instantiate(ballLayerTwo, spawnPoint.transform.position, Quaternion.identity,UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects()[0].transform);
                        Rigidbody rig = b.GetComponent<Rigidbody>();
                        rig.AddForce(spawnPoint.forward * force);
                        ballQueue.RemoveAt(0);
                        FindObjectOfType<AudioManager>().Play("BallStart");
                        //FindObjectOfType<AudioManager>().Play("Launch");
                    }
                }
            }
            

            yield return new WaitForSeconds(spawnDelay);
        }
    }
    public void SpawnBall(int type)
    {
        
        if (canSpawn)
        {
            if (type == 0)
            {
                ballQueue.Add(0);
            }
            else
            {
                ballQueue.Add(1);
            }
        }
    }

    public void StartGame()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
        }
        spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    public void StopGame()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
        }
    }

    void OnStart()
    {
        if (!gameStarted)
        {
            print("start");
            gameStarted = true;
            
        }
    }

    public void ClearQueue()
    {
        ballQueue.Clear();
    }
    
}
