using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingSceneController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI money;

    private void Start()
    {
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != SceneType.HappyEndScene.ToString())
            return;
        
        money.text = GameManager.instance.dataManager.playerData.money.ToString();
    }

    public void ResetEndChangeScene()
    {
        GameManager.instance.dataManager.ResetData();
        GameManager.instance.sceneManager.ChangeScene(SceneType.TitleScene.ToString());
    }

    public void ContinueGame()
    {
        GameManager.instance.sceneManager.ChangeScene(SceneType.MainScene.ToString());
    }
}
