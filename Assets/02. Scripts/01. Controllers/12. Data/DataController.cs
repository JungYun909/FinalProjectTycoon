using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    private Coroutine playerTimeCoroutine;
    private void Start()
    {
        GameManager.instance.dataManager.InitSet();
        GameManager.instance.sceneManager.sceneInfo += MainSceneDataSet;
        Debug.Log(GameManager.instance.dataManager.playerTimeData.deliverySec);
        DontDestroyOnLoad(gameObject);
    }

    private void MainSceneDataSet(SceneType type)
    {
        if(type != SceneType.MainScene)
            return;
        
        GameManager.instance.dataManager.LoadInstallationData();
        
        if (playerTimeCoroutine == null)
            playerTimeCoroutine = StartCoroutine(SaveTimeRoutine());
    }
    
    public IEnumerator SaveTimeRoutine()
    {
        while (true)
        {
            GameManager.instance.dataManager.SaveTimeData();
            Debug.Log(GameManager.instance.dataManager.playerTimeData.deliverySec);
            yield return new WaitForSeconds(3f);
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log(GameManager.instance.dataManager.playerTimeData.deliverySec);
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
