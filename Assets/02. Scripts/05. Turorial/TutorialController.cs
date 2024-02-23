using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private List<TutorialBase> tutorials;
    //[SerializeField] private string nextSceneName = "";

    private TutorialBase curTutorial = null;
    public AudioClip tutoClearSound;

    //private int curIndex = -1;

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
            //GameManager.instance.audioManager.PlaySFX(tutoClearSound);
        }

        if (GameManager.instance.dataManager.playerData.tutoNum >= tutorials.Count - 1)
        {
            CompletedAllTutorials();
            return;
        }

        GameManager.instance.dataManager.playerData.tutoNum++;
        curTutorial = tutorials[GameManager.instance.dataManager.playerData.tutoNum];

        curTutorial.Enter();
    }

    public void CompletedAllTutorials()
    {
        curTutorial = null;

        // 튜토리얼이 모두 끝난 후 넣을 코드 추가 작성
        Debug.Log("튜토리얼 끗!");
        GameManager.instance.audioManager.PlaySFX(tutoClearSound);

        //if(!nextSceneName.Equals(""))
        //{
        //    GameManager.instance.sceneManager.ChangeScene("SAScene2");
        //}
    }
}
