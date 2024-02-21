using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialDestinationMission_2 : TutorialBase
{
    InstallationDestinationController controller;

    public override void Enter()
    {
        base.Enter();

        var destinationcontroller = GameManager.instance.dataManager.curInstallations[1].transform.GetChild(5);
        destinationcontroller.gameObject.SetActive(true);
        controller = destinationcontroller.GetComponent<InstallationDestinationController>();
        controller.OnDestinationEvent += Check;
    }

    private void Check(GameObject startObj, GameObject EndObj)
    {
        if (startObj == GameManager.instance.dataManager.curInstallations[1] && EndObj == GameManager.instance.dataManager.kitchenDoor)
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
        SpawnImmediately();
    }

    private void SpawnImmediately()
    {
        InstallationInventoryController inventoryController = GameManager.instance.dataManager.curInstallations[1].GetComponentInChildren<InstallationInventoryController>();
        inventoryController.spawnTimer = 999;
        inventoryController.eventTimer = 999;
    }

}
