using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallationSetController : MonoBehaviour
{
    public GameObject curGameObject;

    public void InstallationDestroy()
    {
        GameManager.instance.poolManager.DeSpawnFromPool(curGameObject);
        GameManager.instance.installationManager.installationManageController.SetActive(false);
        GameManager.instance.dataManager.SaveInstallation(curGameObject);
        GameManager.instance.dataManager.SaveData();
        curGameObject = null;
    }

    public void InstallationInstall()
    {
        GameManager.instance.installationManager.installationManageController.SetActive(false);
        GameManager.instance.dataManager.PosUpdate(curGameObject);
        GameManager.instance.dataManager.SaveData();
        curGameObject = null;
    }
}
