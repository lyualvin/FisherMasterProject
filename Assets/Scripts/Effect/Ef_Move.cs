using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_Move : MonoBehaviour
{
    public float speed;
    public Vector3 Dir = Vector3.right;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Dir * speed * Time.deltaTime);
    }
}
