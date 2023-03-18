using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorebelObjeckt : MonoBehaviour
{
    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color scoreColor;
    private IngameHighscoreManager ihm;
    AudioManager audioManager;
    public int punkte = 100;

    protected void Awake()
    {
        ihm = FindFirstObjectByType<IngameHighscoreManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    public void Score(Transform transform )
    {

        ihm.Score(punkte, transform, transform.up * 20, scoreColor);
    }

}
