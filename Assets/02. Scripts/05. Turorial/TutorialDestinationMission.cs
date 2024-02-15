using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDestinationMission : TutorialBase
{
    [SerializeField] private string startTarget;
    [SerializeField] private string endTarget;

    public override void Enter()
    {
        base.Enter();
        gameObject.AddComponent<InstallationDestinationController>().OnDestinationEvent += Check;
        
    }

    private void Check(GameObject startObj, GameObject EndObj)
    {
        if(startObj.GetComponent<InstallationController>()._installationData.installasionName.Contains(startTarget)
            && EndObj.GetComponent<InstallationController>()._installationData.installasionName.Contains(endTarget))
            completed = true;
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
