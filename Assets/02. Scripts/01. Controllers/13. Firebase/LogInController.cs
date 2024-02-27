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

    public TextMeshProUGUI outPutTitleText;
    public TextMeshProUGUI outPutText;
    public GameObject messageObj;
    public Button logInBtn;

    private void Start()
    {
        GameManager.instance.firebaseAuthManager.InitSet();
        GameManager.instance.firebaseAuthManager.LogChangeEvent += OnChangeState;
        GameManager.instance.firebaseAuthManager.LogErrorEvent += OnError;
        GameManager.instance.firebaseAuthManager.CreatIDEvent += OnCreate;
    }

    private void OnCreate()
    {
        if(!messageObj.activeSelf)
            messageObj.SetActive(true);
        
        outPutTitleText.text = "계정이 생성되었습니다";
        outPutText.text = "환영합니다, 로그인하여 플레이해주세요";
    }

    private void OnError(Exception e)
    {
        if (!messageObj.activeSelf)
            messageObj.SetActive(true);

        outPutTitleText.text = "에러";
        outPutText.text = e.ToString();
        Debug.Log(e);
    }

    private void OnChangeState(bool sign)
    {
        if (!messageObj.activeSelf)
            messageObj.SetActive(true);
        
        outPutText.text = sign ? "환영합니다 " : "안녕히가세요 ";
        outPutText.text += GameManager.instance.firebaseAuthManager.UserID;
        outPutText.text += "님";

        logInBtn.gameObject.SetActive(sign);
    }

    private void Create()
    {
        GameManager.instance.firebaseAuthManager.Create(email.text, password.text);
    }
    
    private void LogIn()
    {
        GameManager.instance.firebaseAuthManager.LogIn(email.text, password.text);
    }
    
    private void LogOut()
    {
        GameManager.instance.firebaseAuthManager.LogOut();
    }
    
}
