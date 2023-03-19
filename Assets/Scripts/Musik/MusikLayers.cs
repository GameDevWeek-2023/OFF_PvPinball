using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class MusikLayers : MonoBehaviour
{
    [ItemCanBeNull] public List<Rigidbody> rigs;
        
    // Start is called before the first frame update
    private float combinedVel = 0;

    void Start()
    {
        
        StartCoroutine(CalcVel());
    }

    public void AddToList(Rigidbody newrig)
    {
        rigs.Add(newrig);
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
        yield return new WaitForSeconds(0.1f);
    }

    public void RemoveFromList(Rigidbody rig)
    {
        rigs.Remove(rig);   
    }
}
