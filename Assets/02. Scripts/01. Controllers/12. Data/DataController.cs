using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    private Coroutine playerTimeCoroutine;
    private int chargePerDay;
    
    private void OnEnable()
    {
        GameManager.instance.dataManager.InitSet();
        GameManager.instance.sceneManager.sceneInfo += MainSceneDataSet;
        GameManager.instance.statManager.OnMoneyChange += EarnedPerDay;
        GameManager.instance.statManager.onDateChanged += ResetEarnedPerDay;
        DontDestroyOnLoad(gameObject);
    }

    private void ResetEarnedPerDay()
    {
        if (GameManager.instance.dataManager.playerData.earnedPerDay < chargePerDay)
        {
            GameManager.instance.dataManager.playerData.earnedPerDay = chargePerDay;
            GameManager.instance.dataManager.OnMoneyRankUpdate?.Invoke(GameManager.instance.dataManager.playerData.earnedPerDay);
        }
    
        chargePerDay = 0;
    }
    
    private void EarnedPerDay(int money)
    {
        chargePerDay += money;
    }
    
    private void MainSceneDataSet(SceneType type)
    {
        if(type != SceneType.MainScene)
            return;
        GameManager.instance.dataManager.LoadInstallationData();
        GameManager.instance.inventoryManager.InitSet();
        if (playerTimeCoroutine == null)
            playerTimeCoroutine = StartCoroutine(SaveTimeRoutine());
        GameManager.instance.inventoryManager.LoadInventoryData();
        StartCoroutine(GameManager.instance.inventoryManager.SaveAllInventoriesRoutine());
        GameManager.instance.destinationManager.LoadDestinationsData();
        StartCoroutine(GameManager.instance.destinationManager.SaveAllDestinationsRoutine());
    }
    
    public IEnumerator SaveTimeRoutine()
    {
        while (true)
        {
            GameManager.instance.dataManager.SaveTimeData();
            yield return new WaitForSeconds(3f);
        }
    }

    private void OnApplicationQuit()
    {
        GameManager.instance.dataManager.SaveData();

    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if(pauseStatus)
            GameManager.instance.dataManager.SaveData();
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if(hasFocus)
            GameManager.instance.dataManager.SaveData();
    }
}
