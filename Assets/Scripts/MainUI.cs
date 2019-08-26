using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    public GameObject settingPanel;
    public Toggle muteToggle;

    private void Start()
    {
        muteToggle.isOn = !AudioManager.Instance.IsMute;
    }

    public void SwitchMute(bool isOn)
    {
        AudioManager.Instance.SwitchMute(isOn);
    }

    public void OnBackButtonDown()
    {
        PlayerPrefs.SetInt("gold", GameController.Instance.gold);
        PlayerPrefs.SetInt("lv", GameController.Instance.lv);
        PlayerPrefs.SetFloat("smallCountDown", GameController.Instance.smallTimer);
        PlayerPrefs.SetFloat("largeCountDown", GameController.Instance.largeTimer);
        PlayerPrefs.SetInt("exp", GameController.Instance.exp);
        PlayerPrefs.SetInt("mute", (AudioManager.Instance.IsMute==false)? 0 : 1);
        SceneManager.LoadScene(0);
    }

    public void OnSettingButtonDown()
    {
        settingPanel.SetActive(true);
    }

    public void OnCloseButtonDown()
    {
        settingPanel.SetActive(false);

    }
}
