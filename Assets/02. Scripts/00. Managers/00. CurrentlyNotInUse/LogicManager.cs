using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour       //게임매니저 담당? > 게임 흐름? 난이도 관리? > TopDown의 게임매니저 참조
{
    [SerializeField] private int payBackGold;
    private void Start()
    {
        GameManager.instance.statManager.onDateChanged += PayBack;
    }

    private void PayBack()
    {
        Debug.Log(GameManager.instance.statManager.currentGold);
        Debug.Log(payBackGold);
        if (GameManager.instance.statManager.curDebt > 0)
        {
            if (GameManager.instance.statManager.currentGold >= payBackGold)
            {
                GameManager.instance.statManager.SpendGold(payBackGold);
                GameManager.instance.statManager.curWarningCount++;
            }
            else
            {
                GameManager.instance.statManager.SpendGold(GameManager.instance.statManager.currentGold);
                GameManager.instance.statManager.curWarningCount--;
            }
            
        }

        Debug.Log(GameManager.instance.statManager.curWarningCount);
        if (GameManager.instance.statManager.curWarningCount < -2)
        {
            GameManager.instance.sceneManager.ChangeScene(SceneType.EndScene.ToString());
            Debug.Log("YouLose");
        }
        
    }
}
