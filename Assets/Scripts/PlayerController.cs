using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerID = 0;

    public int hitPoints;
    
    public Healthbar healthbar;

    private void Start()
    {
        //healthbar.InitHitPoints(playerID, hitPoints);
    }

    public void OnDamage()
    {
        hitPoints--;
        //healthbar.RemoveHeart(playerID);
    }
}
