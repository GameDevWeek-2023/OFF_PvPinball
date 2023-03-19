using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class MusikLayers : MonoBehaviour
{
    public GameController gameController;
    [SerializeField] public GameObject[] ballListe;
    [ItemCanBeNull] private List<Rigidbody> rigs;
    public List<GameObject> ballslist;
        
    // Start is called before the first frame update
    private float combinedVel = 0;

    void Start()
    {
        
        StartCoroutine(CalcVel());
    }

    public void RefreshList(Rigidbody newrig)
    {
        ballslist = gameController.startPipeLeft.balls;

        foreach (GameObject ball in ballslist)
        {
            rigs.Add(newrig);
        }
    }
    // Update is called once per frame
    void Update()
    {
        print(combinedVel);
    }

    public float calculateCombinedForce(List<Rigidbody> list)
    {
        float sum = 0;
        foreach (Rigidbody _rig in rigs)
        {
            if (!_rig)
            {
                return sum;
            }
            else
            {
                sum += _rig.velocity.magnitude;
            }
        }

        return sum;
    }

    IEnumerator CalcVel()
    {
        combinedVel = calculateCombinedForce(rigs);
        yield return null;
    }
}
