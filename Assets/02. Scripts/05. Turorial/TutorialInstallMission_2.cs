using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInstallMission_2 : TutorialBase
{
    [SerializeField] private Transform clearZone;

    public override void Enter()
    {
        base.Enter();
        GameManager.instance.dataManager.PosUpdateEvent += Check;
        clearZone.gameObject.SetActive(true);
    }

    private void Check(Vector3 curPosition)
    {
        if(clearZone.position == curPosition)
            completed = true;
    }

    public override void Execute(TutorialController tutorialController)
    {
        if (completed == true) tutorialController.SetNextTutorial();
    }

    public override void Exit()
    {
        base .Exit();
        clearZone.gameObject.SetActive(false);
        GameManager.instance.dataManager.PosUpdateEvent -= Check;
    }
}
