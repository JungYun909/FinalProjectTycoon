using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class EndingSceneController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI shopName;
    [SerializeField] private TextMeshProUGUI money;
    [SerializeField] private TextMeshProUGUI day;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI debt;
    

    private void Start()
    {
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != SceneType.HappyEndScene.ToString())
            return;
        
        shopName.text = GameManager.instance.dataManager.playerData.shopName.ToString();
        money.text = GameManager.instance.dataManager.playerData.money.ToString();
        day.text = GameManager.instance.dataManager.playerData.day.ToString();
        level.text = GameManager.instance.dataManager.playerData.level.ToString();
        debt.text = GameManager.instance.dataManager.playerData.debt.ToString();
        
    }

    public void ResetEndChangeScene()
    {
        GameManager.instance.dataManager.ResetData();
        GameManager.instance.dataManager.SaveData();
        GameManager.instance.sceneManager.ChangeScene(SceneType.TitleScene.ToString());
    }

    public void ContinueGame()
    {
        GameManager.instance.sceneManager.ChangeScene(SceneType.MainScene.ToString());
    }
}
