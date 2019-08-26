using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    public void NewGame()
    {
        PlayerPrefs.DeleteKey("gold");
        PlayerPrefs.DeleteKey("lv");
        PlayerPrefs.DeleteKey("exp");
        PlayerPrefs.DeleteKey("smallCountDown");
        PlayerPrefs.DeleteKey("largeCountDown");
        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(1); 
    }
}
