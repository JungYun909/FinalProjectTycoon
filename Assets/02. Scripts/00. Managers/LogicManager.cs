using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour       //게임매니저 담당? > 게임 흐름? 난이도 관리? > TopDown의 게임매니저 참조
{
    [SerializeField] private int payBackGold;

    public bool happyEnd;
    public UIBase questPrefab;
    public event Action DebtCompensated;
    public int paidAmount;
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
        if (GameManager.instance.dataManager.playerData.happilyEnded)
        {
            DebtCompensated?.Invoke();
            return;
        }
        
        if (GameManager.instance.dataManager.playerData.debt > 0)
        {
            if (GameManager.instance.dataManager.playerData.money >= payBackGold)
            {
                paidAmount = payBackGold;
                GameManager.instance.statManager.SpendGold(payBackGold);
                GameManager.instance.dataManager.playerData.debt -= payBackGold;
                GameManager.instance.dataManager.playerData.warningCount++;
                if (GameManager.instance.dataManager.playerData.warningCount >= 0)
                    GameManager.instance.dataManager.playerData.warningCount = 0;
            }
            else if(GameManager.instance.dataManager.playerData.money >=500)
            {
                paidAmount = 500;
                GameManager.instance.statManager.SpendGold(500);
                GameManager.instance.dataManager.playerData.debt -= 500;
                GameManager.instance.dataManager.playerData.warningCount--;
            }
            else
            {
                paidAmount = 0;
                GameManager.instance.dataManager.playerData.warningCount--;
            }
        }

        else
        {
            HappyEnding();
        }

        if (GameManager.instance.dataManager.playerData.warningCount < -2)
        {
            GameManager.instance.sceneManager.ChangeScene(SceneType.EndScene.ToString());
        }
        DebtCompensated?.Invoke();
        paidAmount = 0;
    }

    private void HappyEnding()
    {
        if (GameManager.instance.dataManager.playerData.debt > 0)
            return;

        GameManager.instance.dataManager.SaveAllDestinationData(GameManager.instance.destinationManager.destinationControllerID, GameManager.instance.destinationManager.destinationInfo);
        GameManager.instance.dataManager.SaveInventoryData(GameManager.instance.inventoryManager.nextInventoryID, GameManager.instance.inventoryManager.allInventories);
        GameManager.instance.dataManager.playerData.happilyEnded= true;
        GameManager.instance.sceneManager.ChangeScene(SceneType.HappyEndScene.ToString());
    }
}
