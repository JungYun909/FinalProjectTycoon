using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip titleBgm;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.audioManager.PlayMusic(titleBgm);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
