using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_WaterWave : MonoBehaviour
{

    public Texture[] waterWave;
    private Material material;
    private int index=0;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        InvokeRepeating("Change", 0, 0.1f);
    }

    // Update is called once per frame
    void Change()
    {
        material.mainTexture = waterWave[index];
        index = (index + 1) % waterWave.Length;
    }
}
