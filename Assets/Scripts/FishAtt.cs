using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAtt : MonoBehaviour
{ 


    public int maxNum;
    public int maxSpeed;
    public int hp;
    public int exp;
    public int gold;


    public GameObject deathPrefab;
    public GameObject goldPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Border")
        {
            Destroy(gameObject);
        }
    }


    void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            GameController.Instance.gold += gold;
            GameController.Instance.exp += exp;


            GameObject death = Instantiate(deathPrefab);
            death.transform.SetParent(gameObject.transform.parent, false);
            death.transform.position = gameObject.transform.position;
            death.transform.rotation = gameObject.transform.rotation;

            GameObject goldPre = Instantiate(goldPrefab);
            goldPre.transform.SetParent(gameObject.transform.parent, false);
            goldPre.transform.position = gameObject.transform.position;
            goldPre.transform.rotation = gameObject.transform.rotation;

            if (GetComponent<Ef_Effect>() != null)
            {
                AudioManager.Instance.PlayEffSound(AudioManager.Instance.rewardClip);
                GetComponent<Ef_Effect>().PlayEffect();
            }
            Destroy(gameObject);
        }
    }
}
