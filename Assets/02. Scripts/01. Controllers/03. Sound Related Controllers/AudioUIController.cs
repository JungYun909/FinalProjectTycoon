using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioUIController : MonoBehaviour //TODO 오디오 매니저가 아니라 컨트롤러에 간섭해야함
{
    [SerializeField] private Slider _musicSlider, _sfxSlider;
    [SerializeField] private Image bgmImageOrigin;
    [SerializeField] private Image sfxImageOrigin;


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
}