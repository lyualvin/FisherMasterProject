using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_MoveTo : MonoBehaviour
{
    private GameObject goldCollect;
    // Start is called before the first frame update
    void Start()
    {
        goldCollect = GameObject.Find("GoldCollect"); 
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(transform.position, goldCollect.transform.position, Time.deltaTime*5);
    }

}
