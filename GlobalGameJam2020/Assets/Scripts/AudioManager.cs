using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    [Serializable]
    public class AudioItem
    {
        public string Key;
        public AudioClip Clip;
    }

    [Header("CLIPS")]
    [SerializeField]
    private AudioItem[] audioCollection;

    [Header("COMPONENTS")]
    [SerializeField]
    private AudioSource musicSource;

    [SerializeField]
    private AudioSource fxSource;

    [SerializeField]
    private AudioSource specialSource;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void PlayMusic(string key)
    {
        musicSource.clip = getAudioClip(key);
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayFX(string key)
    {
        fxSource.clip = getAudioClip(key);
        fxSource.Play();
    }

    public void StopFX()
    {
        specialSource.Stop();
    }

    public void PlaySpecialFX(string key)
    {
        specialSource.clip = getAudioClip(key);
        specialSource.Play();
    }

    public void StopSpecialFX()
    {
        specialSource.Stop();
    }

    private AudioClip getAudioClip(string key)
    {
        return audioCollection.Select(x => x).Where(c => c.Key == key).FirstOrDefault()?.Clip;
    }
}
