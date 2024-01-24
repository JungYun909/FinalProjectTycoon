using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [Header("InstallationSO")]
    public InstallationData kneader;
    public InstallationData oven;
    public InstallationData conbinator;
    public InstallationData ChocolateMachine;

    [Header("IngredientSO")]
    public IngredientData dough;
    public IngredientData chocolate;
    
    [Header("StandardPrefab")]
    public GameObject installationObj;
    public GameObject ingredientObj;


    public static SpawnManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnInstallaion(InstallationData installationData)
    {
        GameObject spawnInstallationObj = PoolManager.instacne.SpawnFromPool(installationObj);
        spawnInstallationObj.transform.position = new Vector3(0f, 0f, 0f);
        spawnInstallationObj.AddComponent<InstallationController>()._installationData = installationData;
        InstallationAddCompornant(spawnInstallationObj);
    }
    // public void SpawnKneader()
    // {
    //     GameObject spawnInstallationObj = PoolManager.instacne.SpawnFromPool(installationObj);
    //     spawnInstallationObj.transform.position = new Vector3(0f, 0f, 0f);
    //     spawnInstallationObj.AddComponent<InstallationController>()._installationData = kneader;
    //     InstallationAddCompornant(spawnInstallationObj);
    // }
    //
    // public void SpawnOven()
    // {
    //     GameObject spawnInstallationObj = PoolManager.instacne.SpawnFromPool(installationObj);
    //     spawnInstallationObj.transform.position = new Vector3(0f, 0f, 0f);
    //     spawnInstallationObj.AddComponent<InstallationController>()._installationData = oven;
    //     InstallationAddCompornant(spawnInstallationObj);
    // }
    //
    // public void SpawnConbinator()
    // {
    //     GameObject spawnInstallationObj = PoolManager.instacne.SpawnFromPool(installationObj);
    //     spawnInstallationObj.transform.position = new Vector3(0f, 0f, 0f);
    //     spawnInstallationObj.AddComponent<InstallationController>()._installationData = conbinator;
    //     InstallationAddCompornant(spawnInstallationObj);
    // }

    private void InstallationAddCompornant(GameObject curObject)
    {
        if(curObject.GetComponent<InstallationController>() == null)
            return;

        InstallationController installationController = curObject.GetComponent<InstallationController>();
        
        if(installationController._installationData.canSpawn)
            curObject.AddComponent<InstallationSpawnController>();

        if (installationController._installationData.haveDoughInventory)
        {
            curObject.AddComponent<Inventory>();
            curObject.AddComponent<InventoryController>();
        }
    }

    public void Spawn(GameObject spawningInstallationObj, string spawnObjName)
    {
        //풀매니저에서 소환하고 
        GameObject curSpawnObj = PoolManager.instacne.SpawnFromPool(ingredientObj);
        curSpawnObj.transform.position = spawningInstallationObj.transform.position + ((spawningInstallationObj.GetComponent<InstallationController>()._installationData.destinationInstallation.transform.position - spawningInstallationObj.transform.position).normalized);
        curSpawnObj.AddComponent<IngredientOnColliderController>();
        
        switch (spawnObjName)
        {
            case "Kneader":
                curSpawnObj.AddComponent<IngredientController>()._ingredientData = dough;
                curSpawnObj.tag = "Dough";
                break;
            case "ChocolateMachine":
                curSpawnObj.AddComponent<IngredientController>()._ingredientData = chocolate;
                curSpawnObj.tag = "Ingredient";
                break;
            default:
                Debug.Log("Does not exist");
                break;
        }

        if (spawningInstallationObj.GetComponent<InstallationController>() != null && curSpawnObj.GetComponent<IngredientController>()._ingredientData.canMove)
        {
            MovementController movementController = curSpawnObj.AddComponent<MovementController>();
            movementController.Move(spawningInstallationObj.GetComponent<InstallationController>()._installationData.destinationInstallation);
        }

    }
}
