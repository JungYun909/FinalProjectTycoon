using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDialog : TutorialBase
{
    //캐릭터들의 대사를 진행하는 다이얼로그시스템
    private DialogSystem dialogSystem;

    public override void Enter()
    {
        dialogSystem = GetComponent<DialogSystem>();
        dialogSystem.Setup();
    }

    public override void Execute(TutorialController tutorialController)
    {
        //현재 분기에 진행되는 대사 진행
        bool isCompleted = dialogSystem.UpdateDialog();

        //현재 분기의 대사 진행이 완료되면
        if(isCompleted == true)
        {
            //다음 튜토리얼 이동
            tutorialController.SetNextTutorial();
        }
    }

    public override void Exit()
    {
    }
}
