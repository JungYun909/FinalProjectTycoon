using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardTimeControll : MonoBehaviour
{
    private TMP_Text timeTxt;
    private float sec = 0;
    private int min = 30;

    [SerializeField] private Button rewardBtn;

    private void Awake()
    {
        timeTxt = GetComponentInChildren<TMP_Text>();
    }

    void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if (gameObject.activeSelf)
        {
            sec -= Time.deltaTime;
            
            timeTxt.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);
            if((int)sec < 0)
            {
                min--;
                sec = 59;
            }
        }
        if (min <= 0 && sec <= 0)
        {
            timeTxt.text = "00:00";
            gameObject.SetActive(false);
            rewardBtn.gameObject.SetActive(true);
            min = 30;
            return;
        }
    }
}
