using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableObject : MonoBehaviour
{
    public GameObject prefab;

    [Range(0f, 1f)]
    public float spawnChance;
}
