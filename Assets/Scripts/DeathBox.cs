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
    public Teleport teleporter;
    
    public bool isLeft = true;
    private void OnTriggerEnter(Collider other)
    {
        print("Deathbox OneTriggerEnter");
        if (other.gameObject.GetComponent<Ball>() != null)
        {
            if (!other.GetComponent<Ball>().isLayerTwo)
            {
                print("Debug : Should Spawn Ghosts Balls");
                teleporter.spawnghost();
            }
            else
            {
                if (this.isLeft)
                {
                    gameController.OnHitLeft();
                }
                else
                {
                    gameController.OnHitRight();
                }
                pipe.RemoveGhostBall(other.gameObject);
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


        if (gameController.hitPointsLeft <= 0)
        {
            //gameController.EndGame(true);
        }

        if (gameController.hitPointsRight <= 0)
        {
            //gameController.EndGame(true);
        }
    }
}
