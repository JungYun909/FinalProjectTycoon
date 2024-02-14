using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialClickMission : TutorialBase
{
    [SerializeField] private GameObject fingerIcon;
    [SerializeField] private Button[] missionBtn;

    public bool isClick = false;
    public bool isClickAllCompleted = false;
    public override void Enter()
    {
        // 유저 클릭 가능
        isClick = true;
        // 지시하는 손가락 오브젝트 활성화
        fingerIcon.SetActive(true);

        for (int i = 1; i < missionBtn.Length; i++)
        {
            missionBtn[i].gameObject.SetActive(false);
        }
    }

    public override void Execute(TutorialController tutorialController)
    {
        if (GameManager.instance.uiManager.lastUIName.Contains("Shop_Furniture")) tutorialController.SetNextTutorial();
    }

    public override void Exit()
    {
        // 유저 클릭 불가능
        isClick = false;
        // 지시하는 손가락 오브젝트 비활성화
        fingerIcon.SetActive(false);

        for (int i = 1; i < missionBtn.Length; i++)
        {
            missionBtn[i].gameObject.SetActive(true);
        }
    }
}
