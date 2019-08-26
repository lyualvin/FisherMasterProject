using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebAtt : MonoBehaviour
{
    public float disappearTime;

    public int damage;

    private void Start()
    {
        Destroy(gameObject, disappearTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Fish")
        {
            collision.SendMessage("TakeDamage",damage);
        }
    }
}
