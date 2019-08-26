using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get { return _instance; }
    }


    public AudioSource bgmAudio;
    public AudioClip seaWaveClip;
    public AudioClip goldClip;
    public AudioClip rewardClip;
    public AudioClip fireClip;
    public AudioClip changeClip;
    public AudioClip lvUpClip;

    private bool isMute = false;

    public bool IsMute { get => isMute; }
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        isMute = (PlayerPrefs.GetInt("mute",0) == 0 ? false: true);
        DoMute();
    }

    public void SwitchMute(bool isOn)
    {
        isMute = !isOn;
        DoMute();
    }

    void DoMute()
    {
        if (isMute)
        {
            bgmAudio.Pause();
        }
        else
        {
            bgmAudio.Play();
        }

    }

    public void PlayEffSound(AudioClip clip)
    {

        if (!isMute)
        {
            AudioSource.PlayClipAtPoint(clip, new Vector3(0,0,5));
        }
    }

}
