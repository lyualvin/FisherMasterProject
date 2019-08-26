using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAtt : MonoBehaviour
{
    public float speed;
    public int damage; 

    public GameObject webPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Border")
        {
            Destroy(gameObject);
        }
        else if(collision.tag == "Fish")
        {
            GameObject go =  Instantiate(webPrefab);
            go.transform.SetParent(gameObject.transform.parent, false);
            go.transform.position = gameObject.transform.position;
            go.GetComponent<WebAtt>().damage = damage;
            Destroy(gameObject);
        }
    }
}
