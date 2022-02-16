using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject csPanel;

    public GameObject pauseGamePanel;

    [Header("Audio")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    private bool muteOn;
    public Toggle muteOnToggle;
    public AudioClip[] playlist;
    private int firstRun;

    [Header("Save Game Texts")]
    public Text loadSave1Text;
    public Text loadSave2Text;
    public Text loadSave3Text;

    public Text saveSave1Text;
    public Text saveSave2Text;
    public Text saveSave3Text;

    private void Start()
    {
        pauseGamePanel.SetActive(false);
        csPanel.SetActive(false);

        CheckAudioAtStart();

        PrintCharacterNameOnLoadButtons();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        //if (!musicSource.isPlaying)
        //{
        //    SetAudioClip();
        //    musicSource.Play();
        //}
    }

    public void ActivateDeactivateInventoryPanel()
    {
        csPanel.SetActive(!csPanel.activeSelf);
    }

    public void MusicVolumeChange()
    {
        float v = muteOn ? 0 : musicVolumeSlider.value; //ako je "mute == true" onda je "v = 0", else je Slider.value

        musicSource.volume = v;
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
    }
    public void SFXVolumeChange()
    {
        float v = muteOn ? 0 : sfxVolumeSlider.value; //ako je "mute == true" onda je "v = 0", else je Slider.value

        sfxSource.volume = v;
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
    }
    public void MuteUnmuteChange()
    {
        muteOn = !muteOn;

        if (muteOn)
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
        musicSource.clip = playlist[Random.Range(0, playlist.Length)];
    }
    private void CheckAudioAtStart()
    {
        firstRun = PlayerPrefs.GetInt("FirstRun");

        if (firstRun == 0)
        {
            firstRun = 1;
            PlayerPrefs.SetInt("FirstRun", firstRun);

            muteOn = false;
            muteOnToggle.isOn = false;

            musicVolumeSlider.value = 0.5f;
            sfxVolumeSlider.value = 1;

            PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
            PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);

            musicSource.volume = musicVolumeSlider.value;
            sfxSource.volume = sfxVolumeSlider.value;
        }
        else
        {
            if (PlayerPrefs.GetInt("Mute") == 0)
            {
                muteOn = false;
                muteOnToggle.isOn = false;
            }
            else if (PlayerPrefs.GetInt("Mute") == 1)
            {
                muteOn = true;
                muteOnToggle.isOn = true;
            }

            musicSource.volume = muteOn ? 0 : PlayerPrefs.GetFloat("MusicVolume"); //ako je mute onda je vol=0, ako nije onda je vol=playerPrefs
            sfxSource.volume = muteOn ? 0 : PlayerPrefs.GetFloat("SFXVolume"); //ako je mute onda je vol=0, ako nije onda je vol=playerPrefs

            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
    }

    private void PauseGame()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            pauseGamePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseGamePanel.SetActive(false);
        }
    }

    public void OpenMainMenuFromGame()
    {
        SceneManager.LoadScene(0);
        //SceneManager.LoadSceneAsync(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PrintCharacterNameOnLoadButtons()
    {
        if (PlayerPrefs.HasKey("PlayerName1"))
        {
            loadSave1Text.text = $"Save 1 {PlayerPrefs.GetString("PlayerName1")}";
            saveSave1Text.text = $"Save 1 {PlayerPrefs.GetString("PlayerName1")}";
        }
        else
        {
            loadSave1Text.text = "Save 1";
            saveSave1Text.text = "Save 1";
        }
        if (PlayerPrefs.HasKey("PlayerName2"))
        {
            loadSave2Text.text = $"Save 2 {PlayerPrefs.GetString("PlayerName2")}";
            saveSave2Text.text = $"Save 2 {PlayerPrefs.GetString("PlayerName2")}";
        }
        else
        {
            loadSave2Text.text = "Save 2";
            saveSave2Text.text = "Save 2";
        }
        if (PlayerPrefs.HasKey("PlayerName3"))
        {
            loadSave3Text.text = $"Save 3 {PlayerPrefs.GetString("PlayerName3")}";
            saveSave3Text.text = $"Save 3 {PlayerPrefs.GetString("PlayerName3")}";
        }
        else
        {
            loadSave3Text.text = "Save 3";
            saveSave3Text.text = "Save 3";
        }
    }
}
