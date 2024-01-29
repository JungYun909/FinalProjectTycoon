using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallationInstallController : MonoBehaviour
{
    public InstallationController controller;

    private void Start()
    {
        controller.installationFuctionSet += installFunction;
        installFunction();
    }

    private void installFunction()
    {
        Debug.Log("1");

        GameManager.instance.installationManager.installationManageController.SetActive(true);
        GameManager.instance.installationManager.installationManageController.transform.position = gameObject.transform.position;
        GameManager.instance.installationManager.installationManageController
            .GetComponentInChildren<InstallationMoveController>().curGameObject = transform.root.gameObject;
        GameManager.instance.installationManager.installationManageController
            .GetComponentInChildren<InstallationSetController>().curGameObject = transform.root.gameObject;
    }
}
