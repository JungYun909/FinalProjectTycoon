using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInstallMission : TutorialBase
{
    [SerializeField] private string targetName;
    public override void Enter()
    {
        base.Enter();
        GameManager.instance.spawnManager.installMachineEvent += Check;
    }

    private void Check(MachineSO curMachineSO)
    {
        if (curMachineSO.installasionName.Contains(targetName))
            completed = true;
        Debug.Log(completed);
    }

    public override void Execute(TutorialController tutorialController)
    {
        if (completed == true) tutorialController.SetNextTutorial();
    }
    
    public override void Exit()
    {
        base.Exit();
        GameManager.instance.spawnManager.installMachineEvent -= Check;
    }


}
