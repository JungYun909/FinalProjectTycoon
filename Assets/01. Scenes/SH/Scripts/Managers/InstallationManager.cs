using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InstallationManager : MonoBehaviour
{
    private InstallationData _installationData;
    
    public GameObject installationSetUI;
    public GameObject installationManageController;

    public GameObject curInstallation;

    public static InstallationManager instance;

    private void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
        installationSetUI.SetActive(false);
        installationManageController.SetActive(false);
    }

    public void ManageInstallationBtn()
    {
        installationSetUI.SetActive(false);
        installationManageController.SetActive(true);
        installationManageController.transform.position = curInstallation.transform.position;
    }

    public void DestinationSetBtn()
    {
        installationSetUI.SetActive(false);
    }

    public void InstallBtn()
    {
        installationManageController.SetActive(false);
        installationSetUI.SetActive(true);
    }
    
    public void DestroyBtn()
    {
        PoolManager.instacne.DeSpawnFromPool(curInstallation);
        installationManageController.SetActive(false);
        curInstallation = null;
    }

    public void BackBtn()
    {
        installationSetUI.SetActive(false);
        curInstallation = null;
    }
}
