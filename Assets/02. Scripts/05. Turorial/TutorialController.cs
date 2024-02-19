using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private List<TutorialBase> tutorials;
    [SerializeField] private string nextSceneName = "";

    private TutorialBase curTutorial = null;
    private int curIndex = -1;
    private int saveIndex = 0;

    private void Start()
    {
        SetNextTutorial();
    }

    private void Update()
    {
        if (curTutorial != null)
        {
            curTutorial.Execute(this);
        }
    }

    public void SetNextTutorial()
    {
        if(curTutorial != null)
        {
            curTutorial.Exit();
        }

        if(curIndex >= tutorials.Count - 1)
        {
            CompletedAllTutorials();
            return;
        }

        curIndex++;
        curTutorial = tutorials[curIndex];
        saveIndex++;

        curTutorial.Enter();
    }

    public void CompletedAllTutorials()
    {
        curTutorial = null;

        // 튜토리얼이 모두 끝난 후 넣을 코드 추가 작성
        Debug.Log("튜토리얼 끗!");

        if(!nextSceneName.Equals(""))
        {
            GameManager.instance.sceneManager.ChangeScene("SAScene2");
        }
    }
}
