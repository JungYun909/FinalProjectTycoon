using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class TutorialOpenInventory : TutorialBase
{
    StandInventoryUI inventoryUI;

    public override void Enter()
    {
        base.Enter();
    }


    public override void Execute(TutorialController tutorialController)
    {
        if (GameManager.instance.uiManager.lastUIName.Contains("StandInventory")) tutorialController.SetNextTutorial();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
