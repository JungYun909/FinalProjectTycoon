using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour       //게임매니저 담당? > 게임 흐름? 난이도 관리? > TopDown의 게임매니저 참조
{
    [SerializeField] private int payBackGold;

    public bool happyEnd;
    public UIBase questPrefab;
    public event Action<int> DebtCompensated;
    private int paidAmount;
    private void Start()
    {
        GameManager.instance.statManager.onDateChanged += PayBack;
    }

    private void OnDisable()
    {
        GameManager.instance.statManager.onDateChanged -= PayBack;
    }

    private void PayBack()
    {
        if(happyEnd)
            return;
        
        HappyEnding();

        if (GameManager.instance.dataManager.playerData.debt > 0)
        {
            if (GameManager.instance.dataManager.playerData.money >= payBackGold)
            {
                paidAmount = payBackGold;
                GameManager.instance.statManager.SpendGold(payBackGold);
                GameManager.instance.dataManager.playerData.debt -= payBackGold;
                GameManager.instance.dataManager.playerData.warningCount++;
                DebtCompensated?.Invoke(paidAmount);
            }
            else if(GameManager.instance.dataManager.playerData.money >=500)
            {
                GameManager.instance.statManager.SpendGold(500);
                GameManager.instance.dataManager.playerData.debt -= 500;
                GameManager.instance.dataManager.playerData.warningCount--;
                
            }
            else if (GameManager.instance.dataManager.playerData.money >= 500)
            {
                paidAmount = 0;
                GameManager.instance.dataManager.playerData.warningCount--;
            }
        }
        DebtCompensated?.Invoke(paidAmount);

        if (GameManager.instance.dataManager.playerData.warningCount < -2)
        {
            GameManager.instance.sceneManager.ChangeScene(SceneType.EndScene.ToString());
            GameManager.instance.dataManager.ResetData();
            Debug.Log("YouLose");
        }
        
    }

    private void HappyEnding()
    {
        if (GameManager.instance.dataManager.playerData.debt > 0)
            return;

        happyEnd = true;
        GameManager.instance.sceneManager.ChangeScene(SceneType.HappyEndScene.ToString());
    }
}
