using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDestinationMission : TutorialBase
{
    InstallationDestinationController controller;

    private TutorialController controller_;
    public override void Enter()
    {
        base.Enter();

        var destinationcontroller = GameManager.instance.dataManager.curInstallations[0].transform.GetChild(5);
        destinationcontroller.gameObject.SetActive(true);
        controller = destinationcontroller.GetComponent<InstallationDestinationController>();
        controller.OnDestinationEvent += Check;
    }

    private void Check(GameObject startObj, GameObject EndObj)
    {
        if(startObj == GameManager.instance.dataManager.curInstallations[0] && EndObj == GameManager.instance.dataManager.curInstallations[1])
            completed = true;
        else if (startObj == GameManager.instance.dataManager.curInstallations[1] && EndObj == GameManager.instance.dataManager.entrance)
            completed = true;
    }

    public override void Execute(TutorialController tutorialController)
    {
        if (completed == true) tutorialController.SetNextTutorial();
    }

    public override void Exit()
    {
        base.Exit();
        controller.OnDestinationEvent -= Check;
    }
}
