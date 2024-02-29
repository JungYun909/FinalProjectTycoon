using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneController : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public GameObject warning;
    public Button startBtn;
    public Button nameSetBtn;

    private void Start()
    {
        InitSet();
    }

    private void InitSet()
    {
        if (GameManager.instance.dataManager.playerData.shopName != "")
        {
            nameInputField.gameObject.SetActive(false);
            nameSetBtn.gameObject.SetActive(false);
            startBtn.gameObject.SetActive(true);
            
        }
        else
        {
            nameInputField.gameObject.SetActive(true);
            nameSetBtn.gameObject.SetActive(true);
            startBtn.gameObject.SetActive(false);
        }
        
        warning.SetActive(false);
    }

    public void OnNameSetBtn()
    {
        if (!CanName(nameInputField.text) || nameInputField.text == "")
        {
            warning.SetActive(true);
            return;
        }

        GameManager.instance.dataManager.playerData.shopName = nameInputField.text;
        nameInputField.gameObject.SetActive(false);
        nameSetBtn.gameObject.SetActive(false);
        startBtn.gameObject.SetActive(true);
    }

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
        InitSet();
    }

    private bool CanName(string text)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(text, "^[가-힣]*$");
    }
}
