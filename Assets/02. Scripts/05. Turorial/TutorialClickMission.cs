using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialClickMission : TutorialBase
{
    [SerializeField] private GameObject FingerIcon;

    public bool isClick = false;
    public bool isClickAllCompleted = false;
    public override void Enter()
    {
        // 유저 클릭 가능
        isClick = true;
        // 지시하는 손가락 오브젝트 활성화
        FingerIcon.SetActive(true);
    }

    public override void Execute(TutorialController tutorialController)
    {
        if (isClickAllCompleted == true) tutorialController.SetNextTutorial(); 
    }

    public override void Exit()
    {
        // 유저 클릭 불가능
        isClick = false;
        // 지시하는 손가락 오브젝트 비활성화
        FingerIcon.SetActive (false);
    }
}
