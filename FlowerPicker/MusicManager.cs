using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource mainAudioSource; //ovaj audiosource je od music managera
    public AudioSource beeAudioSource; //ovaj audiosource je na pceli
    public AudioSource flowerAudioSource; //ovaj audiosource je na parent dijelu cvijeta koji nije clickable i ne moze nestati
    public AudioSource victoryAudioSource; //ovaj audiosource je SAMO za victory sound

    [Header("Audio Settings")]
    public Slider musicVolumeSlider; //ovaj slider kontrolira volume za muziku
    public Slider sfxVolumeSlider; //ovaj slider kontrolira volume za efekte, odnosno za zvukove od pcele i cvijeta
    public bool muteOn; //ovaj bool sluzi da se glazba i(!!!) sfx muteaju

    [Header("Audio Clips")]
    public AudioClip song1;
    public AudioClip song2;
    public AudioClip victoryClip; //tu je clip koji svira kada se dogodi pobjeda

    private void Awake()
    {
        mainAudioSource.GetComponent<AudioSource>(); //tu je sve jasno
        mainAudioSource.loop = false; //NE SMIJE BITI LOOP inace nece nikada ponovno povuci pjesmu iz random playliste
    }

    private void Start()
    {
        musicVolumeSlider.value = 0.25f; //pocetna vrijednost na kojoj ce biti volume muzike
        sfxVolumeSlider.value = 0.75f; //pocetna vrijednost na kojoj ce biti volume sfxa

        mainAudioSource.volume = musicVolumeSlider.value; //tu je sve jasno, to je da se seta na istu vrijednost
        victoryAudioSource.volume = musicVolumeSlider.value;
        beeAudioSource.volume = sfxVolumeSlider.value;
        flowerAudioSource.volume = sfxVolumeSlider.value;

        mainAudioSource.clip = song1; //da pocne svirati taj clip na pocetku

        mainAudioSource.mute=false;
    }

    private void Update()
    {
        VolumeChange(); //tu je u updateu da prati volume change stalno

        if (!mainAudioSource.isPlaying) //iduca pjesma iz playliste ce poceti svirati samo ako je prethodna zavrsila i audiosource vise ne svira nista
        {
            SetAudioClip(); //promjena pjesme koja ce svirati
            mainAudioSource.Play();
        }
    }

    public void SetAudioClip() //za promjenu pjesme unutar playliste
    {
        if (mainAudioSource.clip == song1)
        {
            mainAudioSource.clip = song2;
        }
        else if (mainAudioSource.clip == song2)
        {
            mainAudioSource.clip = song1;
        }
    }

    public void VictoryFanfare()
    {
        mainAudioSource.mute = true;
        victoryAudioSource.Play();
    }

    public void MuteOn() //povezano sa toggle elementom na UI, a sluzi da kontrolira da li je volume 0 ili nije
    {
        muteOn = !muteOn; //dakle on click ce se dogoditi izmjena vrijednosti iz true u false ili obrnuto
    }

    public void VolumeChange()
    {
        float volM = muteOn ? 0 : musicVolumeSlider.value; //ako je muteOn true onda je float 0, else je Slider.value
        float volX = muteOn ? 0 : sfxVolumeSlider.value; //ako je muteOn true onda je float 0, else je Slider.value

        mainAudioSource.volume = volM; //povezuje sami volume audiosourcea sa vrijednosti koja je prethodno definirana
        victoryAudioSource.volume = volM;
        beeAudioSource.volume = volX;
        flowerAudioSource.volume = volX;
    }

    public void BeeSound() //da se moze pozvati zvuk pcele kada je potrebno da se odsvira
    {
        beeAudioSource.Play();
    }

    public void FlowerSound() //da se moze pozvati zvuk otpadanja latica kada je potrebno da odsvira
    {
        flowerAudioSource.Play();
    }
}
