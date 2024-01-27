using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioUIController : MonoBehaviour //TODO 오디오 매니저가 아니라 컨트롤러에 간섭해야함
{
    public Slider _musicSlider, _sfxSlider;

    // public void ToggleMusic()
    // {
    //     AudioManager.Instance.ToggleMusic();
    // }
    //
    // public void ToggleSFX()
    // {
    //     AudioManager.Instance.ToggleSFX();
    // }

    public void MusicVolume()
    {
        AudioManager.instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.instance.SFXVolume(_sfxSlider.value);
    }
}