using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardTimeControll : MonoBehaviour
{
    private TMP_Text timeTxt;
    //private float sec = 0;
    //private int min = 10;

    [SerializeField] private Button rewardBtn;

    private void Awake()
    {
        timeTxt = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        CurDeliveryState();
    }

    void Update()
    {
        Timer();
    }

    private void CurDeliveryState()
    {
        if(GameManager.instance.dataManager.playerData.deliveryClear == false)
        {
            return;
        }
        rewardBtn.gameObject.SetActive(true);
    }

    private void Timer()
    {
        if (gameObject.activeSelf)
        {
            GameManager.instance.dataManager.playerTimeData.deliverySec -= Time.deltaTime;
            
            timeTxt.text = string.Format("{0:D2}:{1:D2}", GameManager.instance.dataManager.playerTimeData.deliveryMin, (int)GameManager.instance.dataManager.playerTimeData.deliverySec);
            if((int)GameManager.instance.dataManager.playerTimeData.deliverySec < 0)
            {
                GameManager.instance.dataManager.playerTimeData.deliveryMin--;
                GameManager.instance.dataManager.playerTimeData.deliverySec = 59;
            }
        }
        if (GameManager.instance.dataManager.playerTimeData.deliveryMin <= 0 && GameManager.instance.dataManager.playerTimeData.deliverySec <= 0)
        {
            timeTxt.text = "00:00";
            gameObject.SetActive(false);
            rewardBtn.gameObject.SetActive(true);
            GameManager.instance.dataManager.playerTimeData.deliveryMin = 10;
            GameManager.instance.dataManager.playerData.deliveryClear = true;
            GameManager.instance.dataManager.playerData.deliveryStart = false;
            GameManager.instance.dataManager.SaveData();
            return;
        }
    }
}
