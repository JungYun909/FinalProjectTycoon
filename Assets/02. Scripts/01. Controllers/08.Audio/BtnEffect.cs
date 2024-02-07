using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnEffect : MonoBehaviour
{
    public void BtnSound(AudioClip effect)
    {
        GameManager.instance.audioManager.PlaySFX(effect);
    }
}
