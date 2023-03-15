using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerController : MonoBehaviour
{
    public SpawnableObject[] spawnableObjects;

    public GameObject katapult;
    
    public Transform[] KatapultSpawnPoints;


    private void Start()
    {
        StartCoroutine(SpawnKatapults());
    }

    IEnumerator SpawnKatapults()
    {
        while (true)
        {
            int randomSpawnPoint = Random.Range(0, KatapultSpawnPoints.Length - 1);

            GameObject kat = Instantiate(katapult, KatapultSpawnPoints[randomSpawnPoint].position, Quaternion.identity, KatapultSpawnPoints[randomSpawnPoint]);

            kat.GetComponent<Katapult>().direction = KatapultSpawnPoints[randomSpawnPoint].forward;
            yield return new WaitForSeconds(2f);
        }
    }
}
