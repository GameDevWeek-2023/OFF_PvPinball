using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public TextMeshProUGUI hpLeft;
    public TextMeshProUGUI hpRight;

    public int maxHitPoints;

    private int hitPointsPlayerLeft, hitPointsPlayerRight;

    private List<GameObject> hitPointsLeft = new List<GameObject>(); 
    private List<GameObject> hitPointsRight = new List<GameObject>(); 

    public GameObject Heart;
    
    public void InitHitPoints(int player, int hp)
    {
        if (player == 0)
        {
            hitPointsPlayerLeft = hp;
            hpLeft.text = hitPointsPlayerLeft.ToString();
        }
        else
        {
            hitPointsPlayerRight = hp;
            hpRight.text = hitPointsPlayerRight.ToString();
        }

        /*
        if (player == 0)
        {
            for (int i = 0; i < hitPointsPlayerLeft; i++)
            {
                GameObject left = Instantiate(Heart, barPlayerLeft);
                Image image = left.GetComponent<Image>();
                image.color = Color.blue;
                hitPointsLeft.Add(left);
            }
        }

        else
        {
            for (int i = 0; i < hitPointsPlayerRight; i++)
            {
                GameObject right = Instantiate(Heart, barPlayerRight);
                Image image = right.GetComponent<Image>();
                image.color = Color.green;
                hitPointsRight.Add(right);
            }
        }*/
        
    }
    
    // 0 left, 1 right
    public void RemoveHeart(int player)
    {
        /*if (player == 0)
        {
            if (hitPointsPlayerLeft != null && hitPointsLeft.Count > 0)
            {
                GameObject bla = hitPointsLeft[0];
                hitPointsLeft.RemoveAt(0);
                Destroy(bla);
            }
        }
        else
        {
            if (hitPointsPlayerRight != null && hitPointsRight.Count > 0)
            {
                GameObject bla = hitPointsRight[0];
                hitPointsRight.RemoveAt(0);
                Destroy(bla);
            }
        }*/
        
        if (player == 0)
        {
            hitPointsPlayerLeft--;
            hpLeft.text = hitPointsPlayerLeft.ToString();
        }

        else
        {
            hitPointsPlayerRight--;
            hpRight.text = hitPointsPlayerRight.ToString();
        }
    }

    public void LooseHp(int player)
    {
        
    }
}
