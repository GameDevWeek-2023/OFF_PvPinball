using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    public PlayerController player;
    public Pipe pipe;
    public GameController gameController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Ball>() != null)
        {
            if (!other.GetComponent<Ball>().isLayerTwo)
            {
                pipe.SpawnBall(1);
                Destroy(other.gameObject);
            }
            else
            {
                player.OnDamage();
            }

            bool isLeft = other.gameObject.GetComponent<Ball>().isLeftPlayer;
            Destroy(other.gameObject);
            CheckBalls(isLeft);
        }
    }

    private void CheckBalls(bool isLeftPlayer)
    {
        bool isLastLeftBall = true;
        bool isLastRightBall = true;

        if (GameObject.FindGameObjectsWithTag("Ball").Length <= 1)
        {
            gameController.EndGame(isLeftPlayer);
            return;
        }
        
        print(GameObject.FindGameObjectsWithTag("Ball").Length);
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball")) 
        {
            if (ball.GetComponent<Ball>().isLeftPlayer)
            {
                isLastLeftBall = false;
            }
            else
            {
                isLastRightBall = false;
            }
        }

        if (isLastLeftBall)
        {
            gameController.EndGame(true);
        }

        if (isLastRightBall)
        {
            //gameController.EndGame(false);
        }
    }
}
