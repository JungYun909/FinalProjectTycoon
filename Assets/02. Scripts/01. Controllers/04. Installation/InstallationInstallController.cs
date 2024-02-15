using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
        GameObject installUI = GameManager.instance.installationManager.installationManageController;
        InstallationSetController installUIController = installUI.GetComponent<InstallationSetController>();
        if (installUIController.curGameObject != null)
        {
            return;
        }
        
        installUI.SetActive(true);
        installUI.transform.position = gameObject.transform.position;
        installUIController.curGameObject = transform.root.gameObject;
    }
}
