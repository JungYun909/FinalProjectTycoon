using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LogInController : MonoBehaviour
{
    public TMP_InputField email;
    public TMP_InputField password;
    public TMP_InputField shopName;
    
    public TextMeshProUGUI outPutText;
    public Button logInBtn;

    public GameObject shopNameSetUI;
    public GameObject logInInput;
    public GameObject logIn;
    public GameObject createBtn;
    public GameObject shopNameBtn;
    public GameObject console;
    
    public Button startBtn;

    public event Action OnNamingEvent;

    private void Start()
    {
        GameManager.instance.firebaseAuthManager.InitSet();
        GameManager.instance.firebaseAuthManager.LogChangeEvent += OnChangeState;
        GameManager.instance.firebaseAuthManager.LogErrorEvent += OnError;
        GameManager.instance.firebaseAuthManager.CreatIDEvent += OnCreate;
    }

    private void OnCreate()
    {
        logInInput.SetActive(false);
        shopNameSetUI.SetActive(true);
        createBtn.SetActive(false);
        shopNameBtn.SetActive(true);
        string text = "가게 이름이 뭐야?";
        ConsoleSet(text);
    }

    public void OnNaming()
    {
        if (!CanName(shopName.text) || shopName.text == "")
        {
            ConsoleSet("불가능한 이름이에요");
            return;
        }

        GameManager.instance.dataManager.playerData.shopName = shopName.text;
        logIn.SetActive(false);
        startBtn.gameObject.SetActive(true);
        GameManager.instance.firebaseDatabaseManager.SaveData();
    }
    

    private void OnError(Exception e)
    {
        ConsoleSet(e.ToString());
    }

    private void OnChangeState(bool sign)
    {
        if (GameManager.instance.dataManager.playerData.shopName != "")
        {
            if (!console.gameObject.activeSelf || logInBtn.gameObject.activeSelf)
            {
                console.gameObject.SetActive(true);
                logInBtn.gameObject.SetActive(false);
            }
            string text = sign ? "안녕 " : "잘가 ";
            text += GameManager.instance.dataManager.playerData.shopName;
            ConsoleSet(text);   
            logInBtn.gameObject.SetActive(sign);
        }
        else
        {
            logInInput.SetActive(false);
            shopNameSetUI.SetActive(true);
            createBtn.SetActive(false);
            shopNameBtn.SetActive(true);
            string text = "가게 이름이 뭐야?";
            ConsoleSet(text);
        }
    }

    public void Create()
    {
        GameManager.instance.firebaseAuthManager.Create(email.text, password.text);
    }
    
    public void LogIn()
    {
        GameManager.instance.firebaseAuthManager.LogIn(email.text, password.text);
    }
    
    public void LogOut()
    {
        GameManager.instance.firebaseAuthManager.LogOut();
    }

    public void ConsoleSet(string text)
    {
        outPutText.text = text;
    }

    private bool CanName(string text)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(text, "^[가-힣]*$");
    }

}
