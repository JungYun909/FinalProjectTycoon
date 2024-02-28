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

    private GameObject usingBtn;
    private RectTransform backBtnPos;
    private List<RectTransform> btnsPos = new List<RectTransform>();

    private bool OnBtn;
    private bool Move;
    
    private Vector2 backBtnDestination;
    private Vector2 backBtnPosition;

    private void Start()
    {
        backBtnPos = backBtn.GetComponent<RectTransform>();
        backBtn.interactable = false;
        
        backBtnDestination = new Vector2(backBtnPos.anchoredPosition.x - 150, backBtnPos.anchoredPosition.y);
        backBtnPosition = new Vector2(backBtnPos.anchoredPosition.x, backBtnPos.anchoredPosition.y);
        
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

    public void BackBtn()
    {
        OnBtn = false;
        OffBtns();
        backBtn.interactable = false;
        
        GameManager.instance.uiManager.CloseAll();
        
        foreach (var pos in btnsPos)
        {
            if (usingBtn != pos.gameObject)
            {
                Vector2 btnDestination = new Vector2(pos.anchoredPosition.x, pos.anchoredPosition.y + 150);
                StartCoroutine(moveBtn(pos, btnDestination));
            }
        }
        
        StartCoroutine(moveBtn(backBtnPos, backBtnPosition));
        
        usingBtn.GetComponent<Image>().color = Color.white;
        usingBtn = null;
    }
    
    public void ButtonEffect(RectTransform curBtnPos)
    {
        OnBtn = true;
        OffBtns();
        
        usingBtn = curBtnPos.gameObject;
        usingBtn.GetComponent<Image>().color = Color.red;
        
        StartCoroutine(moveBtn(backBtnPos, backBtnDestination));
        
        foreach (var pos in btnsPos)
        {
            if (usingBtn != pos.gameObject)
            {
                Vector2 btnDestination = new Vector2(pos.anchoredPosition.x, pos.anchoredPosition.y - 150);
                StartCoroutine(moveBtn(pos, btnDestination));
            }
        }
    }

    private IEnumerator moveBtn(RectTransform movePos, Vector2 desPos)
    {
        while (Vector2.Distance(movePos.anchoredPosition,desPos) > 10f)
        {
            Move = true;
            movePos.anchoredPosition =
                Vector2.Lerp(movePos.anchoredPosition, desPos, speed);

            yield return null;
        }

        if (Move)
        {
            Move = false;
            Debug.Log("10");
            yield return new WaitForSeconds(1f);
            
            if (OnBtn)
            {
                Debug.Log("1010");
                backBtn.interactable = true;
            }
            else
            {
                OnBtns();
            }
        }
    }
    
    private void OffBtns()
    {
        foreach (var btn in btns)
        {
            btn.interactable = false;
        }
    }
    
    private void OnBtns()
    {
        foreach (var btn in btns)
        {
            btn.interactable = true;
        }
    }

    public void LoadScene(string sceneType)
    {
        GameManager.instance.sceneManager.ChangeScene(sceneType);
    }
}
