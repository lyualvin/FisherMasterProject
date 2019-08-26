using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_SeaWave : MonoBehaviour
{
    private Vector3 tmp; 
    // Start is called before the first frame update
    void Start()
    {
         tmp = -transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, tmp, 10*Time.deltaTime);
    }
}
