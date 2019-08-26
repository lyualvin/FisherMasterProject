using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_Effect : MonoBehaviour
{
    public GameObject[] effectPrefabs;

    public void PlayEffect()
    {
        foreach(var eff in effectPrefabs)
        {
            Instantiate(eff);
        }
    }
}
