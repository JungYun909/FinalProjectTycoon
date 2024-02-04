using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallationInstallController : MonoBehaviour
{
    public InstallationController controller;

    public void InitSet()
    {
        controller.installationFuctionSet += installFunction;
        installFunction();
    }

    private void installFunction()
    {
        GameManager.instance.installationManager.installationManageController.SetActive(true);
        GameManager.instance.installationManager.installationManageController.transform.position = gameObject.transform.position;
        GameManager.instance.installationManager.installationManageController
            .GetComponentInChildren<InstallationSetController>().curGameObject = transform.root.gameObject;
    }
}
