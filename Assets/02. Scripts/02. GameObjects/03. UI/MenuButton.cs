using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.Rendering;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private List<Button> btns = new List<Button>();
    [SerializeField] private float speed;
    [SerializeField] private Button backBtn;
    private bool onBtn;
    private Button usingBtn;
    private RectTransform backBtnPos;
    private List<RectTransform> btnsPos = new List<RectTransform>();
    

    private void Start()
    {
        backBtnPos = backBtn.GetComponent<RectTransform>();
        backBtn.interactable = false;
        
        foreach (var btn in btns)
        {
            btnsPos.Add(btn.GetComponent<RectTransform>());
        }
    }

    public void OpenUI(UIBase openUI)
    {
        GameManager.instance.uiManager.OpenWindow(openUI);
    }

    public void InstallationFunctionIndexSet(int index)
    {
        GameManager.instance.interactionManager.installationFunctionIndex = index;
    }

    public void OnButton(Button curBtn)
    {
        if(onBtn == true)
        {
            return;
        }
        usingBtn = curBtn;
        usingBtn.interactable = false;
        usingBtn.GetComponent<Image>().color = Color.red;

        BtnPositionSet(backBtn, backBtnPos, -150, 0, true);

        for (int i = 0; i < btnsPos.Count; i++)
        {
            if (usingBtn == btns[i])
                continue;
            
            BtnPositionSet(btns[i], btnsPos[i], 0, -150, false);
        }
        onBtn = true;
    }
    
    public void BackBtn()
    {
        if (onBtn == false)
        {
            return;
        }
        GameManager.instance.uiManager.CloseAll();

        BtnPositionSet(backBtn, backBtnPos, 150, 0, false);
        
        for (int i = 0; i < btnsPos.Count; i++)
        {
            if(usingBtn == btns[i])
                continue;
            
            BtnPositionSet(btns[i], btnsPos[i], 0, 150, true);
        }
        
        usingBtn.GetComponent<Image>().color = Color.white;
        usingBtn.interactable = true;
        usingBtn = null;
        onBtn = false;
    }
    


    private void BtnPositionSet(Button btn ,RectTransform pos, int movePosX, int movePosY, bool btnSet)
    {
        Vector2 btnDestination = new Vector2(pos.anchoredPosition.x + movePosX, pos.anchoredPosition.y + movePosY);
        StartCoroutine(MoveBtn(btn, pos, btnDestination, btnSet));
    }

    private IEnumerator MoveBtn(Button btn, RectTransform movePos, Vector2 desPos, bool btnSet)
    {
        btn.interactable = false;
        
        while (Vector2.Distance(movePos.anchoredPosition,desPos) > 10f)
        {
            movePos.anchoredPosition =
                Vector2.Lerp(movePos.anchoredPosition, desPos, speed);

            yield return null;
        }

        btn.interactable = btnSet;
    }
    

    public void LoadScene(string sceneType)
    {
        GameManager.instance.sceneManager.ChangeScene(sceneType);
    }
}
