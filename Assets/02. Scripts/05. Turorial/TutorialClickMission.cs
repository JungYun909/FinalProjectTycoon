using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialClickMission : TutorialBase
{
    [SerializeField] private GameObject fingerIcon;
    [SerializeField] private Button[] missionBtn;
    [SerializeField] private Button targetBtn;

    public bool isClick = false;

    public override void Enter()
    {
        base.Enter();
        // 유저 클릭 가능
        isClick = true;
        // 지시하는 손가락 오브젝트 활성화
        fingerIcon.SetActive(true);

        for (int i = 0; i < missionBtn.Length; i++)
        {
            missionBtn[i].gameObject.SetActive(false);
        }
        targetBtn.gameObject.SetActive(true);
    }

    public override void Execute(TutorialController tutorialController)
    {
        if ( completed == true) tutorialController.SetNextTutorial();
    }

    public override void Exit()
    {
        base.Exit();
        // 유저 클릭 불가능
        isClick = false;
        // 지시하는 손가락 오브젝트 비활성화
        fingerIcon.SetActive(false);

        for (int i = 0; i < missionBtn.Length; i++)
        {
            missionBtn[i].gameObject.SetActive(true);
        }
    }
}
