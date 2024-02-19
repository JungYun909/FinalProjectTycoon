using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialClickMission_2 : TutorialBase
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute(TutorialController tutorialController)
    {
        if (completed == true) tutorialController.SetNextTutorial();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
