using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class M_BallSpawner : NetworkBehaviour
{
    public Transform exitPoint;
    
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


    public override void OnStartServer()
    {
        base.OnStartServer();
        LoadBalls();
        StartCoroutine(SpawnRoutine());
        canShoot = true;
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
    


    [Server]
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
                        GameObject b = Instantiate(ball, exitPoint.transform.position, Quaternion.identity, null);
                        b.gameObject.GetComponent<Rigidbody>().AddForce(exitPoint.forward * spawnForce, ForceMode.Impulse);
                        NetworkServer.Spawn(b);
                        ballQueue.RemoveAt(0);
                    }
                    
                }
                else
                {
                    if (canShoot)
                    {
                        GameObject b = Instantiate(ballLayerTwo, exitPoint.transform.position, Quaternion.identity, null);
                        b.gameObject.GetComponent<Rigidbody>().AddForce(exitPoint.forward * spawnForce, ForceMode.Impulse);
                        NetworkServer.Spawn(b);
                        ballQueue.RemoveAt(0);
                    }
                }
                
                
            }
            

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
