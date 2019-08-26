using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMaker : MonoBehaviour
{
    public Transform fishHolder;
    public Transform[] genPos;
    public GameObject[] fishPrefabs;

    private float FishMakeTime = 0.3f;
    private float FishWaitTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MakeFishes", 0, FishMakeTime);
    }

    void MakeFishes()
    {
        int posIndex = Random.Range(0, genPos.Length);
        int fishIndex = Random.Range(0, fishPrefabs.Length);

        int maxNum = fishPrefabs[fishIndex].GetComponent<FishAtt>().maxNum;
        int maxSpeed = fishPrefabs[fishIndex].GetComponent<FishAtt>().maxSpeed;

        int num = Random.Range(maxNum/2+1 , maxNum);
        int speed = Random.Range(maxSpeed / 2, maxSpeed);

        int moveType = Random.Range(0, 2);
        int angOffset; 
        int angSpeed;
        // 0 为直行， 1 为转向
        if (moveType == 0)
        {
            angOffset = Random.Range(-22,22);
            StartCoroutine(GenStraightFish(posIndex, fishIndex,num, speed, angOffset));
        }
        else
        {
            if(Random.Range(0,2) == 0)
            {
                angSpeed = Random.Range(-15, -9);
            }
            else
            {
                angSpeed = Random.Range(9, 15);
            }
            StartCoroutine(GenTurnFish(posIndex, fishIndex, num, speed, angSpeed)); 
        }

    }

    IEnumerator GenStraightFish(int posIndex, int fishIndex, int num, int speed, int angOffset)
    {
        for(int i = 0; i < num; i++)
        {
            
            GameObject go =  Instantiate(fishPrefabs[fishIndex]);
            go.transform.SetParent(fishHolder, false);
            go.transform.localPosition = genPos[posIndex].localPosition;
            go.transform.localRotation = genPos[posIndex].localRotation;
            go.transform.Rotate(0, 0, angOffset);
            go.GetComponent<SpriteRenderer>().sortingOrder += i;
            go.AddComponent<Ef_Move>().speed = speed;
            yield return new WaitForSeconds(FishWaitTime);
        }
    }

    IEnumerator GenTurnFish(int posIndex, int fishIndex, int num, int speed, int angSpeed)
    {
        for (int i = 0; i < num; i++)
        {

            GameObject go = Instantiate(fishPrefabs[fishIndex]);
            go.transform.SetParent(fishHolder, false);
            go.transform.localPosition = genPos[posIndex].localPosition;
            go.transform.localRotation = genPos[posIndex].localRotation; 
            go.GetComponent<SpriteRenderer>().sortingOrder += i;
            go.AddComponent<Ef_Move>().speed = speed;
            go.AddComponent<Ef_AutoRot>().speed = angSpeed;


            yield return new WaitForSeconds(FishWaitTime);
        }
    }


}
