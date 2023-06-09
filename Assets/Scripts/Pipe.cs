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

    public List<GameObject> balls = new List<GameObject>();
    public List<GameObject> ghostBalls = new List<GameObject>();

    private bool canSpawn = true;

    public int startBalls;
    public int startGhostBalls;

    private bool gameStarted = false;

    private Coroutine spawnRoutine;
    public bool canShoot;

    public bool isLeftPlayer = true;

    public Teleport telescript;
    public void LoadBalls()
    {
        balls = new List<GameObject>();
        ghostBalls = new List<GameObject>();
        
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
        if (collision.gameObject.GetComponent<Ball>() != null)
        {
            StartCoroutine(TravelBall(collision.gameObject.GetComponent<Ball>().isLayerTwo));
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Ball>() != null)
        {
            StartCoroutine(TravelBall(other.gameObject.GetComponent<Ball>().isLayerTwo));
            Destroy(other.gameObject);
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
        
        b.gameObject.GetComponent<Rigidbody>().AddForce(exitPoint.forward * spawnForce, ForceMode.Impulse);
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
                        print("Spawing ball");
                        GameObject b = Instantiate(ball,exitPoint.transform.position,Quaternion.identity,null);
                        
                        Rigidbody rig = b.GetComponent<Rigidbody>();
                        rig.AddForce(exitPoint.forward * force, ForceMode.Impulse);
                        ballQueue.RemoveAt(0);
                        FindObjectOfType<AudioManager>().Play("BallStart");
                        
                        b.GetComponent<Ball>().isLeftPlayer = isLeftPlayer;
                        b.GetComponent<Ball>().isLayerTwo = false;
                        
                        AddBall(b);
                        
                        //FindObjectOfType<AudioManager>().Play("Launch");
                    }
                    
                }
                else
                {
                    if (canShoot)
                    {
                        GameObject b = Instantiate(ballLayerTwo, exitPoint.transform.position, Quaternion.identity, null);
                        Rigidbody rig = b.GetComponent<Rigidbody>();
                        rig.AddForce(exitPoint.forward * force, ForceMode.Impulse);
                        ballQueue.RemoveAt(0);
                        FindObjectOfType<AudioManager>().Play("BallStart");
                        
                        b.GetComponent<Ball>().isLeftPlayer = isLeftPlayer;
                        b.GetComponent<Ball>().isLayerTwo = true;
                        
                        AddGhostBall(b);
                        
                        //FindObjectOfType<AudioManager>().Play("Launch");
                    }
                }
            }
            
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void RemoveBall(GameObject ball)
    {
        balls.Remove(ball);
    }

    public void RemoveGhostBall(GameObject ball)
    {
        ghostBalls.Remove(ball);
    }

    public void AddBall(GameObject ball)
    {
        print(ball.GetComponent<Rigidbody>());
        balls.Add(ball);
    }

    public void AddGhostBall(GameObject ball)
    {
        balls.Add(ball);
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
                
            }
        }
    }

    public void StartGame()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
        }
        ClearQueue();
        LoadBalls();
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
