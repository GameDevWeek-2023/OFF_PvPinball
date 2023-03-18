using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusikLayers : MonoBehaviour
{
    [SerializeField] public GameObject[] ballListe;

    private Rigidbody[] rigs;
    // Start is called before the first frame update
    private float combinedVel = 0;

    void Start()
    {

        for (int i = 0; i < ballListe.Length; i++)
        {
            rigs[i] = ballListe[i].GetComponent<Rigidbody>();
        }

            StartCoroutine(CalcVel());
    }

    // Update is called once per frame
    void Update()
    {
        print(combinedVel);
    }

    public float calculateCombinedForce(Rigidbody[] list)
    {
        float sum = 0;
        for(int i = 0; i < list.Length ; i++)
        {
            sum += list[i].velocity.magnitude;
        }

        return sum;
    }

    IEnumerator CalcVel()
    {
        combinedVel = calculateCombinedForce(rigs);
        yield return null;
    }
}
