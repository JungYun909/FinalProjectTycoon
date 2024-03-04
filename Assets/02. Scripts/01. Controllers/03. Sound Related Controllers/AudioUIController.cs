using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioUIController : MonoBehaviour //TODO 오디오 매니저가 아니라 컨트롤러에 간섭해야함
{
    [SerializeField] private Slider _musicSlider, _sfxSlider;
    [SerializeField] private Image bgmImageOrigin;
    [SerializeField] private Image sfxImageOrigin;

    private void Awake()
    {
        SoundUIInitSet();
    }

    public void ToggleMusic()
    {
        GameManager.instance.audioManager.ToggleMusic(bgmImageOrigin);
    }

    public void ToggleSFX()
    {
        GameManager.instance.audioManager.ToggleSFX(sfxImageOrigin);
    }

    public void MusicVolume()
    {
        GameManager.instance.audioManager.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume()
    {
        GameManager.instance.audioManager.SFXVolume(_sfxSlider.value);
    }

    public void SoundUIInitSet()
    {
        _musicSlider.value = GameManager.instance.audioManager.musicSource.volume;
        for (int i = 0; i < _sfxSlider.value; i++)
        {
            _sfxSlider.value = GameManager.instance.audioManager.sfxSource[i].volume;
        }
        if (GameManager.instance.audioManager.musicSource.mute)
            bgmImageOrigin.sprite = GameManager.instance.audioManager.soundImage[1];
        else
            bgmImageOrigin.sprite = GameManager.instance.audioManager.soundImage[0];
        for (int i = 0; i < _sfxSlider.value; i++)
        {
            if (GameManager.instance.audioManager.sfxSource[i].mute)
                sfxImageOrigin.sprite = GameManager.instance.audioManager.soundImage[1];
            else
                sfxImageOrigin.sprite = GameManager.instance.audioManager.soundImage[0];
        }
    }
}