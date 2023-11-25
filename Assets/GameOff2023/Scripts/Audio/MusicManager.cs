using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//Requrie an audio source so that we can play the stuff
[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{

    public static MusicManager musicManager;

    [Header("Components")]
    [SerializeField]
    private AudioSource audioSource1;
    [SerializeField]
    private AudioSource audioSource2;
    private AudioSource currentAudioSource;

    [Header("References")]
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private string musicVolumeParameter = "MusicVolume";
    [SerializeField]
    private AudioMixerGroup music1;
    [SerializeField]
    private AudioMixerGroup music2;
    private string music1VolumeParameter = "Music1Volume";
    private string music2VolumeParameter = "Music2Volume";

    [Header("Audio Fading Settings")]
    [SerializeField]
    private float timeToFade = 1.0f;
    [SerializeField]
    private float maxVolume = 1.0f; //Replace later with settings manager

    [Header("Music Files")]
    [SerializeField]
    private AudioClip[] currentMusicList;

    // Start is called before the first frame update
    private void Awake()
    {
        if (musicManager == null)
        {
            musicManager = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        //For simplicity sake. Audio source 1 is always the first playing audio source
        currentAudioSource = audioSource1;


    }

    private void Start()
    {

        //load the correct audio levels from PlayerPreferences

        int muteToggleValue = PlayerPrefs.GetInt("MuteToggle", 1);
        if (muteToggleValue == 1)
        {
            Debug.Log("Volume is muted!");
            //SetVolume("MasterVolume", 0);
            MusicManager.musicManager.SetVolume("MasterVolume", 0);
        }

        else if (muteToggleValue == 0)
        {
            SetVolume("MasterVolume", PlayerPrefs.GetFloat("MasterVolume", 1));
        }

        SetVolume("MusicVolume", PlayerPrefs.GetFloat("MusicVolume", 1));

        //sound
        SetVolume("SoundVolume", PlayerPrefs.GetFloat("SoundVolume", 1));
    }

    private void Update()
    {
        if (!currentAudioSource.isPlaying && currentMusicList.Length > 0)
        {
            //Play a new song as soon as the last one finished. Should be a random song from the current list.
            PlayRandomMusicFromList();
        }
    }

    public void StopMusic()
    {
        currentAudioSource.Stop();
        audioSource1.Stop();
        audioSource2.Stop();
        Array.Clear(currentMusicList, 0, currentMusicList.Length);
    }
    //Play a random song from the currentMusicList
    private void PlayRandomMusicFromList()
    {
        //Chose randomly from the array of music to play what music to play. 
        if (currentMusicList.Length == 0)
        {
            Debug.LogWarning("No music has been set in the currentMusicList");
            return;
        }
        currentAudioSource.clip = currentMusicList[UnityEngine.Random.Range(0, currentMusicList.Length - 1)];
        currentAudioSource.Play();

    }

    //transition between the current list and the new music list.
    public void TransitionBetweenMusicLists(AudioClip[] musicList)
    {
        //set the correct music list
        currentMusicList = musicList;

        //Set the different audio sources and start the transition
        if (currentAudioSource == audioSource1)
        {
            currentAudioSource = audioSource2;

            //fade out the music on the currently playing audio mixer group
            StartCoroutine(StartFade(audioMixer, music1VolumeParameter, timeToFade, 0.0f));

            //fade in the music for the new audio mixer group
            StartCoroutine(StartFade(audioMixer, music2VolumeParameter, timeToFade, PlayerPrefs.GetFloat("MusicVolume", 1)));
        }

        else if (currentAudioSource == audioSource2)
        {
            currentAudioSource = audioSource1;

            //fade out the music on the currently playing audio mixer group
            StartCoroutine(StartFade(audioMixer, music2VolumeParameter, timeToFade, 0.0f));

            //fade in the music for the new audio mixer group
            StartCoroutine(StartFade(audioMixer, music1VolumeParameter, timeToFade, PlayerPrefs.GetFloat("MusicVolume", 1)));
        }

        //Start playing the music as it gets faded in by the audiomixer
        PlayRandomMusicFromList();


    }

    //Play a specific song
    private void PlayMusic(AudioClip musicToPlay)
    {
        currentAudioSource.clip = musicToPlay;
        currentAudioSource.Play();
    }


    private IEnumerator StartFade(AudioMixer audioMixer, string audioGroup, float duration, float targetVolume)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(audioGroup, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(audioGroup, Mathf.Log10(newVol) * 20);
            yield return null;
        }
        yield break;
    }

    public void SetVolume(string mixerParameter, float volume)
    {
        //Clamp the value. if the value is 0 then it will break and cause the audio to be back at 100% again. Max of 1, as 1 is 100%
        volume = Mathf.Clamp(volume, 0.001f, 1);
        //converts a float to the correct audio level in the audio mixer
        audioMixer.SetFloat(mixerParameter, Mathf.Log(volume) * 20);
    }
}