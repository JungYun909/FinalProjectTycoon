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
        UpdateObjTag(spawnObjName);
        
        GameObject curSpawnObj = PoolManager.instacne.SpawnFromPool(ingredientObj);
        
        if (PoolManager.instacne.IsMakeNew(curSpawnObj))
            IngredientAddCompornant(curSpawnObj, spawnObjName);
        
        SpawnObjPositionSet(spawningInstallationObj, curSpawnObj);

        SpawnObjMovementSet(spawningInstallationObj, curSpawnObj);
        
        UpdateObjTag(spawnObjName);
    }

    private void IngredientAddCompornant(GameObject curSpawnObj, string spawnObjName)
    {
        curSpawnObj.AddComponent<IngredientOnColliderController>();
        
        switch (spawnObjName)
        {
            case "Kneader":
                curSpawnObj.AddComponent<IngredientController>()._ingredientData = dough;
                break;
            case "ChocolateMachine":
                curSpawnObj.AddComponent<IngredientController>()._ingredientData = chocolate;
                break;
            default:
                Debug.Log("Does not exist");
                break;
        }
    }

    public void SpawnObjPositionSet(GameObject spawningInstallationObj, GameObject curSpawnObj)
    {
        curSpawnObj.transform.position = spawningInstallationObj.transform.position + ((spawningInstallationObj.GetComponent<InstallationController>()._installationData.destinationInstallation.transform.position - spawningInstallationObj.transform.position).normalized);

    }

    public void SpawnObjMovementSet(GameObject spawningInstallationObj,GameObject curSpawnObj)
    {
        if (spawningInstallationObj.GetComponent<InstallationController>() != null && curSpawnObj.GetComponent<IngredientController>()._ingredientData.canMove)
        {
            MovementController movementController = curSpawnObj.AddComponent<MovementController>();
            movementController.Move(spawningInstallationObj.GetComponent<InstallationController>()._installationData.destinationInstallation);
        }
    }

    private void UpdateObjTag(string spawnObjName)
    {
        if (ingredientObj.tag == "Untagged")
        {
            switch (spawnObjName)
            {
                case "Kneader":
                    ingredientObj.tag = "Dough";
                    break;
                default:
                    ingredientObj.tag = "Ingredient";
                    break;
            }
        }
        else
        {
            ingredientObj.tag = "Untagged";
        }
    }
}
