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
    }

    public void InstallationInstall()
    {
        GameManager.instance.installationManager.installationManageController.SetActive(false);
    }
}
