using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperColor : MonoBehaviour
{
    [ColorUsage(true, true)]
    public Color myColor;
    public Material material;

    private void Start()
    {
        material.color = myColor;
    }
}
