using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Audio
{
    public string name;
    public AudioClip clip;
}


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Audio[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource; //TODO 여러개 만 오브젝트 풀링

    // public void Start() //TODO 오디오 컨트롤러로
    // {
    //     PlayMusic("BGM");
    // }
    public void PlayMusic(string name)
    {
        Audio s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Audio s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    // public void ToggleMusic() //TODO 오디오 컨트롤러로 if 문으로 켜져있는지 확인
    // {
    //     musicSource.mute = !musicSource.mute;
    // }
    //
    // public void ToggleSFX()
    // {
    //     sfxSource.mute = !sfxSource.mute;
    // }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume * 0.5f;
    }
    
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
    //
    // public void ClickBtnSound()
    // {
    //     PlaySFX("Select");
    // }
    //
    // public void StartBtnSound()
    // {
    //     PlaySFX("StartBtn");
    // }
}
