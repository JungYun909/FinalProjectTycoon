using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogInController : MonoBehaviour
{
    public TMP_InputField email;
    public TMP_InputField password;
    
    public TextMeshProUGUI outPutText;
    public Button logInBtn;
    public GameObject ShopNameIF;
    public GameObject console;

    private void Start()
    {
        GameManager.instance.firebaseAuthManager.InitSet();
        GameManager.instance.firebaseAuthManager.LogChangeEvent += OnChangeState;
        GameManager.instance.firebaseAuthManager.LogErrorEvent += OnError;
        GameManager.instance.firebaseAuthManager.CreatIDEvent += OnCreate;
    }

    private void OnCreate(string userID)
    {
        ConsoleSet("가게 이름이 뭐야?");
        ShopNameIF.SetActive(true);
    }
    

    private void OnError(Exception e)
    {
        ConsoleSet(e.ToString());
    }

    private void OnChangeState(bool sign)
    {
        string text = sign ? "안녕 " : "잘가 ";
        text += GameManager.instance.dataManager.playerData.shopName;
        
        ConsoleSet(text);   

        logInBtn.gameObject.SetActive(sign);
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
        if (!console.activeSelf)
            console.SetActive(true);

        outPutText.text = text;
    }

    private void AddConsoleText(string text)
    {
        outPutText.text += text;
    }

    private void ResetConsole()
    {
        if (!console.activeSelf)
            console.SetActive(false);

        outPutText.text = "";
    }

}
