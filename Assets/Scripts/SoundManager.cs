using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    public AudioSource soundEffects;
    public AudioSource soundMusic;

    public SoundType[] sounds;

    public bool isMute = false;
    public float volume = 1.0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() //Start Function
    {
        SetVolume(0.5f);
        PlayMusic(Sounds.Music);
    }

    public void Mute(bool status)
    {
        isMute = status;
    }

    public void SetVolume(float volume)
    {
        this.volume = volume;
        soundEffects.volume = volume;
        soundMusic.volume = volume;
    }

    public void PlayMusic(Sounds sound)
    {
        if(isMute)
            return;

        AudioClip clip = GetSoundClip(sound);
        if (clip != null)
        {
            soundMusic.clip = clip;
            soundMusic.Play();
        }
        else
        {
            Debug.LogError("Clip not found for sound type : " + sound);
        }
    }

    public void Play(Sounds sound)
    {
        if(isMute)
            return;

        AudioClip clip = GetSoundClip(sound);
        if (clip != null)
        {
            soundEffects.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("Clip not found for sound type : " + sound);
        }
    }

    private AudioClip GetSoundClip(Sounds sound)
    {
        SoundType item = Array.Find(sounds, item => item.soundTypes == sound);
        if (item != null)
        {
            return item.soundClip;
        }
        return null;
    }
}

[Serializable]
public class SoundType
{
    public Sounds soundTypes;
    public AudioClip soundClip;
}

public enum Sounds
{
    ButtonClicks,
    Music,
    PlayerMove,
    PlayerDeath,
    EnemyDeath
}
