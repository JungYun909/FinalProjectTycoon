using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

//[System.Serializable]
//public class Audio
//{
//    public string name;
//    public AudioClip clip;
//}


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("#BGM")]  //배경음악
    public AudioSource musicSource; //TODO 여러개 만 오브젝트 풀링 씨디플레이어
    public AudioClip bgmClip;

    [Header("#SFX")] // 효과음
    public List<AudioSource> sfxSource = new List<AudioSource>();

    [Header("#동작 여부")]
    private bool onBgm;
    private bool onSfx;


    private void Awake()
    {

        instance = this;
        Init();

    }

    private void Init()
    {
        musicSource.clip = bgmClip;
        musicSource.loop = true;
        onBgm = true;

        for (int i = 0; i < 10; i++)
        {
            AudioSource temp = this.gameObject.AddComponent<AudioSource>();
            sfxSource.Add(temp);
        }

    }


    public void PlayMusic(AudioClip music)
    {
        musicSource.clip = music;
        if (!onBgm)
        {
            return;
        }
   
        musicSource.Play();
    }



    public void PlaySFX(AudioClip effect)
    {
        if (!onSfx) // 효과음이 꺼져있을때 아예 소리 실행안되게
        {
            return;
        }
        bool completePlaying = false; //발견 가능한 소스
        
        for(int i = 0; i < sfxSource.Count; i++)
        {
            if (sfxSource[i].isPlaying)
                continue;
            else
            {
                sfxSource[i].PlayOneShot(effect);
                completePlaying = true; //발견!!
            }
        }

        if(!completePlaying) //모든 오디오 소스가 사용중일때 예외처리
        {
            sfxSource[Random.Range(1, sfxSource.Count)].Stop();
            sfxSource[0].PlayOneShot(effect);
        }
    }

    public void ToggleMusic()
    {
        onBgm = !onBgm;

        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        onSfx = !onSfx;
        for( int i = 0;i < sfxSource.Count;i++)
        {
            sfxSource[i].Stop();
            sfxSource[i].mute = onSfx;
        }
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume * 0.5f;
    }
    
    public void SFXVolume(float volume)
    {
        for(int i = 0;i < sfxSource.Count;i++)
        {
            sfxSource[i].volume = volume;
        }
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
    // 
    // 오디오소스 여러개로 늘리기
    // 여러개로 늘어난걸 알맞게 플레이되게 만들어야대
    // 만약 오디오 소스가 다쓰고있어 그때 예외처리해줘
}
