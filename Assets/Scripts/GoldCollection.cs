using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCollection : MonoBehaviour
{ 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Gold")
        {
            AudioManager.Instance.PlayEffSound(AudioManager.Instance.goldClip);
            Destroy(collision.gameObject);
        }
    }
}
