using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour       //게임매니저 담당? > 게임 흐름? 난이도 관리? > TopDown의 게임매니저 참조
{
    [SerializeField] private int payBackGold;

    public bool happyEnd;
    private void Start()
    {
        GameManager.instance.statManager.onDateChanged += PayBack;
    }

    private void PayBack()
    {
        if(happyEnd)
            return;
        
        HappyEnding();

        if (GameManager.instance.statManager.curDebt > 0)
        {
            if (GameManager.instance.statManager.currentGold >= payBackGold)
            {
                GameManager.instance.statManager.SpendGold(payBackGold);
                GameManager.instance.statManager.shopStat.Debt -= payBackGold;
                GameManager.instance.statManager.curWarningCount++;
            }
            else
            {
                GameManager.instance.statManager.SpendGold(GameManager.instance.statManager.currentGold);
                GameManager.instance.statManager.shopStat.Debt -= GameManager.instance.statManager.currentGold;
                GameManager.instance.statManager.curWarningCount--;
            }
            
        }

        Debug.Log(GameManager.instance.statManager.curWarningCount);
        if (GameManager.instance.statManager.curWarningCount < -2)
        {
            GameManager.instance.sceneManager.ChangeScene(SceneType.EndScene.ToString());
            GameManager.instance.dataManager.ResetData();
            Debug.Log("YouLose");
        }
        
    }

    private void HappyEnding()
    {
        //해피 엔딩 조건
        Debug.Log(GameManager.instance.statManager.shopStat.Debt);
        if (GameManager.instance.statManager.shopStat.Debt > 0)
            return;

        happyEnd = true;
        GameManager.instance.sceneManager.ChangeScene(SceneType.HappyEndScene.ToString());
    }
}
