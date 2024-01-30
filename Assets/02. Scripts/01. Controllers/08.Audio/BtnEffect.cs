using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnEffect : MonoBehaviour
{
    public AudioClip effectClip;

    public void BtnSound()
    {
        GameManager.instance.audioManager.PlaySFX(effectClip);
    }
}
