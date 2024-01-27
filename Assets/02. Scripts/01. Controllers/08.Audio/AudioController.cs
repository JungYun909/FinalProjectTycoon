using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip bgm;

    // Start is called before the first frame update
    void Start()
    {

        AudioManager.instance.PlayMusic(bgm);
    }

}
