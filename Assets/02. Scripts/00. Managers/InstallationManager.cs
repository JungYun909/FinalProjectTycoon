using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InstallationManager : MonoBehaviour
{
    public GameObject installationSetUI;
    public GameObject installationManageController;
    public GameObject doughInventoryUI;
    public GameObject ingredientInventoryUI;

    public GameObject curInstallation;
    public bool onDestination;
    private InstallationController curInstallationController;

    public static InstallationManager instance;

    private void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
        installationSetUI.SetActive(false);
        installationManageController.SetActive(false);
        doughInventoryUI.SetActive(false);
        ingredientInventoryUI.SetActive(false);
    }

    public void OnInstallationSetUI()
    {
        if(curInstallation.GetComponent<InstallationController>() == null)
            return;
            
        curInstallationController = curInstallation.GetComponent<InstallationController>();
        
        installationSetUI.SetActive(true);
        
        if(curInstallationController._installationData.haveDoughInventory)
            doughInventoryUI.SetActive(true);
        else
            doughInventoryUI.SetActive(false);
        
        if(curInstallationController._installationData.haveIngredientInventory)
            ingredientInventoryUI.SetActive(true);
        else
            ingredientInventoryUI.SetActive(false);
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
