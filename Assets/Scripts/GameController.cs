using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance
    {
        get { return _instance; }
    }


    public GameObject[] gunGos; 
    public Transform bulletHolder;

    public Sprite[] bgSprites;
    public Image bgImage;
    public GameObject seaWavaEff;

    public int lv = 0;
    public int exp = 0;
    public int gold = 500;
    public const int largeCountdown = 240;
    public const int smallCountdown = 60;
    public float largeTimer = largeCountdown;
    public float smallTimer = smallCountdown;
    public Color goldColor;

    public GameObject lvUpTips;
    public GameObject fireEff;
    public GameObject changeEff;
    public GameObject lvEff;
    public GameObject goldEff;


    public GameObject[] bullet1Gos;
    public GameObject[] bullet2Gos;
    public GameObject[] bullet3Gos;
    public GameObject[] bullet4Gos;
    public GameObject[] bullet5Gos;

    public Text costText;
    public Text goldText;
    public Text lvText;
    public Text lvNameText;
    public Text smallCountdownText;
    public Text largeCountdownText;

    public Button largeCountdownButton;
    public Button backButton;
    public Button settingButton;
    public Slider expSlider;
     
    //每一炮所需的金币和所造成的伤害
    private int[] oneShootCosts = { 5, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
    private string[] lvName = {"新手","入门","黑铁","青铜","白银","黄金","钻石","大师","王者"};

    private int costIndex = 0;
    private void Start()
    {
        _instance = this ;
        gold = PlayerPrefs.GetInt("gold", gold);
        exp = PlayerPrefs.GetInt("exp", exp);
        lv = PlayerPrefs.GetInt("lv", lv);
        smallTimer = PlayerPrefs.GetFloat("smallCountDown", smallCountdown);
        largeTimer = PlayerPrefs.GetFloat("largeCountDown", largeCountdown);  
        updateUI();
    }
    private void Update()
    {
        BulletCost();
        Fire();
        updateUI();
        ChangeBg();
    } 

    void ChangeBg()
    {
        switch( lv)
        {
            case 20:
                if (bgImage.sprite != bgSprites[1])
                {
                    Instantiate(seaWavaEff);
                    AudioManager.Instance.PlayEffSound(AudioManager.Instance.seaWaveClip);
                    bgImage.sprite = bgSprites[1]; 
                }

                break;
            case 40:
                if (bgImage.sprite != bgSprites[2])
                {
                    Instantiate(seaWavaEff);
                    AudioManager.Instance.PlayEffSound(AudioManager.Instance.seaWaveClip);
                    bgImage.sprite = bgSprites[2];
                }
                break;
            case 80:
                if (bgImage.sprite != bgSprites[3])
                {
                    Instantiate(seaWavaEff);
                    AudioManager.Instance.PlayEffSound(AudioManager.Instance.seaWaveClip);
                    bgImage.sprite = bgSprites[3];
                }
                break; 
            default:
                break;
        }
    }


    void updateUI()
    {
        largeTimer -= Time.deltaTime;
        smallTimer -= Time.deltaTime;
        if(smallTimer <= 0)
        {
            smallTimer = smallCountdown;
            gold += 50;
        }
        if (largeTimer <= 0 && largeCountdownButton.gameObject.activeSelf == false)
        {
            largeCountdownText.gameObject.SetActive(false);
            largeCountdownButton.gameObject.SetActive(true);
        }

        if (exp>= 1000+20*lv)
        { 
            lv++; 
            exp -= 1000 + 200 * (lv - 1); 
            lvUpTips.SetActive(true);
            lvUpTips.transform.Find("Text").GetComponent<Text>().text = lv.ToString();
            StartCoroutine(lvUpTips.GetComponent<Ef_HideSelf>().HideSelf(0.6f));
            AudioManager.Instance.PlayEffSound(AudioManager.Instance.lvUpClip);
            Instantiate(lvEff);

        }
        goldText.text = "$" + gold.ToString();
        lvText.text = lv.ToString();
        if (lv/10 <=9)
        { 
            lvNameText.text = lvName[lv / 10];
        }
        else
        { 
            lvNameText.text = lvName[9];
        }
        smallCountdownText.text = (int)smallTimer / 10 + "  " + (int)smallTimer % 10;
        largeCountdownText.text = (int)largeTimer + "s";
        expSlider.value = (float)exp / (1000 + 200 * lv);
    }



    void Fire()
    {
        GameObject[] useBullets = bullet5Gos;
        int bulletIndex;
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() ==false)
        { 
            if(gold - oneShootCosts[costIndex] >= 0)
            {

                switch (costIndex / 4)
                {
                    case 0: useBullets = bullet1Gos; break;
                    case 1: useBullets = bullet2Gos; break;
                    case 2: useBullets = bullet3Gos; break;
                    case 3: useBullets = bullet4Gos; break;
                    case 4: useBullets = bullet5Gos; break;
                }
                bulletIndex = lv % 10;
                gold -= oneShootCosts[costIndex];
                AudioManager.Instance.PlayEffSound(AudioManager.Instance.fireClip);
                Instantiate(fireEff);
                GameObject go = Instantiate(useBullets[bulletIndex]);
                go.transform.SetParent(bulletHolder, false);
                go.transform.position = gunGos[costIndex / 4].transform.Find("GunPos").transform.position;
                go.transform.rotation = gunGos[costIndex / 4].transform.Find("GunPos").transform.rotation;

                go.GetComponent<BulletAtt>().damage = oneShootCosts[costIndex];

                go.AddComponent<Ef_Move>().Dir = Vector3.up;
                go.GetComponent<Ef_Move>().speed = go.GetComponent<BulletAtt>().speed;
            }
            else
            {
                StartCoroutine(GoldNotEnough());
                //goldText.color = Color.Lerp(goldColor, Color.red, 0.5f);
            }


        }
    }
     
    void BulletCost()
    {
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            PowerDown();
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            PowerUp();
        }
    }

    public void PowerUp()
    {
        gunGos[costIndex / 4].SetActive(false);
        costIndex++;
        AudioManager.Instance.PlayEffSound(AudioManager.Instance.changeClip);
        Instantiate(changeEff);
        costIndex = (costIndex > (oneShootCosts.Length - 1) ? 0 : costIndex);
        gunGos[costIndex / 4].SetActive(true);
        costText.text = "$" + oneShootCosts[costIndex];
    }

    public void PowerDown()
    {
        gunGos[costIndex / 4].SetActive(false);
        costIndex--;
        AudioManager.Instance.PlayEffSound(AudioManager.Instance.changeClip);
        Instantiate(changeEff);
        costIndex = (costIndex < 0 ? (oneShootCosts.Length - 1) : costIndex);
        gunGos[costIndex / 4].SetActive(true);
        costText.text = "$" + oneShootCosts[costIndex];
    }

    public void OnLargeCountdownButtonDown()
    {
        gold += 500;
        AudioManager.Instance.PlayEffSound(AudioManager.Instance.rewardClip);
        Instantiate(goldEff);
        largeCountdownButton.gameObject.SetActive(false);
        largeCountdownText.gameObject.SetActive(true);
        largeTimer = largeCountdown;
    }

    IEnumerator GoldNotEnough()
    {
        //goldText.color = goldColor;
        goldText.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        goldText.color = Color.yellow;
    }
}
