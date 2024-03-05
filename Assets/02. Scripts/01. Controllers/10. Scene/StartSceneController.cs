using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StartSceneController : MonoBehaviour
{
    public void OnStartBtn()
    {
        GameManager.instance.dataManager.SaveData();
        GameManager.instance.sceneManager.ChangeScene(SceneType.MainScene.ToString());
    }
    
    public void OnResetBtn()
    {
        GameManager.instance.dataManager.ResetData();
        GameManager.instance.dataManager.SaveData();
        GameManager.instance.dataManager.ResetInventoryAndDestinationData();
    }
}
