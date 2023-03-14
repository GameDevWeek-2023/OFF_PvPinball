using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Pipe))]
public class MapGeneratorController : Editor
{

    public Pipe pipe;

    private void OnEnable()
    {
        pipe = (Pipe)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (Application.isPlaying)
        {
            if (GUILayout.Button("Spall Layer 1 Ball"))
            {
                pipe.SpawnBall(0);
            }
            if (GUILayout.Button("Spall Layer 2 Ball"))
            {
                pipe.SpawnBall(1);
            }
        }
    }
}

