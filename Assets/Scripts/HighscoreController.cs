using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreController : MonoBehaviour
{
    public GamePreferencesManager gamePreferencesManager;
    public Scrollbar scrollbar;

    public GameObject scorePrefab;
    public Transform container;
    
    private bool hasInnited = false;

    private List<GameObject> prefabs = new List<GameObject>();

    public void FillHighscores()
    {
        foreach (GameObject p in prefabs)
        {
            Destroy(p);    
        }
        CreatePrefabs();
    }
    
    public void CreatePrefabs()
    {
        Dictionary<int, ScoreData> combined = gamePreferencesManager.combinedData;

        foreach (KeyValuePair<int, ScoreData> item in combined.OrderBy(key => key.Value.score).Reverse())
        {
            int key = item.Key;

            if (combined[key].playerName.Length > 0)
            {
               GameObject go = Instantiate(scorePrefab, container);
               if (go.GetComponent<Score>() != null)
               {
                   go.GetComponent<Score>().SetScore(combined[key].score.ToString());
                   go.GetComponent<Score>().SetPlayerName(combined[key].playerName);
               }
               prefabs.Add(go);
            }
        }

        StartCoroutine(InitScrollbar());
    }

    IEnumerator InitScrollbar()
    {
        yield return new WaitForSeconds(0.02f);
        scrollbar.value = 1;
    }
}
