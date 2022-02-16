using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource mainAudioSource;

    [SerializeField]
    private AudioSource sfxAudioSource;

    [SerializeField]
    private Slider mainVolumeSlider;

    [SerializeField]
    private Slider sfxVolumeSlider;

    [SerializeField]
    private bool mute;

    [SerializeField]
    private Toggle muteToggle;

    [SerializeField]
    private AudioClip[] playlist;

    private int firstRun;

    private void Awake()
    {
        mainAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        firstRun = PlayerPrefs.GetInt("FirstRun");

        if (firstRun == 0)
        {
            firstRun = 1;
            PlayerPrefs.SetInt("FirstRun", firstRun);

            mute = false;
            muteToggle.isOn = false;

            mainVolumeSlider.value = 0.5f;
            sfxVolumeSlider.value = 1;

            PlayerPrefs.SetFloat("MainVolume", mainVolumeSlider.value);
            PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);

            mainAudioSource.volume = mainVolumeSlider.value;
            sfxAudioSource.volume = sfxVolumeSlider.value;
        }
        else
        {
            if (PlayerPrefs.GetInt("Mute") == 0)
            {
                mute = false;
                muteToggle.isOn = false;
            }
            else if (PlayerPrefs.GetInt("Mute") == 1)
            {
                mute = true;
                muteToggle.isOn = true;
            }

            mainAudioSource.volume = mute ? 0 : PlayerPrefs.GetFloat("MainVolume"); //ako je mute onda je vol=0, ako nije onda je vol=playerPrefs
            sfxAudioSource.volume = mute ? 0 : PlayerPrefs.GetFloat("SFXVolume"); //ako je mute onda je vol=0, ako nije onda je vol=playerPrefs

            mainVolumeSlider.value = PlayerPrefs.GetFloat("MainVolume");
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
    }

    public void MusicVolumeChange()
    {
        float v = mute ? 0 : mainVolumeSlider.value; //ako je "mute == true" onda je "v = 0", else je Slider.value

        mainAudioSource.volume = v;
        PlayerPrefs.SetFloat("MainVolume", mainVolumeSlider.value);
    }

    public void SFXVolumeChange()
    {
        float v = mute ? 0 : sfxVolumeSlider.value; //ako je "mute == true" onda je "v = 0", else je Slider.value

        sfxAudioSource.volume = v;
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
    }

    public void MuteOn()
    {
        mute = !mute;

        if (mute)
        {
            PlayerPrefs.SetInt("Mute", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Mute", 0);
        }

        MusicVolumeChange();
        SFXVolumeChange();
    }

    private void SetAudioClip()
    {
        mainAudioSource.clip = playlist[Random.Range(0, playlist.Length)];
    }

    private void Update()
    {
        if (!mainAudioSource.isPlaying)
        {
            SetAudioClip();
            mainAudioSource.Play();
        }
    }
}
