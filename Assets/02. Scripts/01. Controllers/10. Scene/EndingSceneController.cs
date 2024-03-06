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
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (currentSceneName == SceneType.EndScene.ToString() || currentSceneName == SceneType.HappyEndScene.ToString())
        {
            shopName.text = GameManager.instance.dataManager.playerData.shopName.ToString();
            money.text = GameManager.instance.dataManager.playerData.totalGoldEarned.ToString();
            day.text = (GameManager.instance.dataManager.playerData.day-1).ToString();
            level.text = GameManager.instance.dataManager.playerData.level.ToString();
            debt.text = GameManager.instance.dataManager.playerData.debt.ToString();

            if (currentSceneName == SceneType.EndScene.ToString())
            {
                GameManager.instance.dataManager.ResetData();
                GameManager.instance.dataManager.SaveData();
                GameManager.instance.dataManager.ResetInventoryAndDestinationData();
            }
        }
    }

    public void ResetEndChangeScene()
    {
        GameManager.instance.dataManager.ResetData();
        GameManager.instance.dataManager.SaveData();
        GameManager.instance.dataManager.ResetInventoryAndDestinationData();
        GameManager.instance.sceneManager.ChangeScene(SceneType.TitleScene.ToString());
    }

    public void ContinueGame()
    {
        GameManager.instance.sceneManager.ChangeScene(SceneType.MainScene.ToString());
    }
}
