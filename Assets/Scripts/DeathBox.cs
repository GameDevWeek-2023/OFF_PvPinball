using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    public PlayerController player;
    public Pipe pipe;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Ball>() != null)
        {
            pipe.SpawnBall(other.GetComponent<Ball>().isLayerTwo? 1 : 0);
            Destroy(other.gameObject);
            player.OnDamage();
            
        }
    }
}
