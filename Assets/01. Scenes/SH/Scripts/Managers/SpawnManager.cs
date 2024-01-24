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

    public void SpawnKneader()
    {
        GameObject spawnInstallationObj = PoolManager.instacne.SpawnFromPool(installationObj);
        spawnInstallationObj.transform.position = new Vector3(0f, 0f, 0f);
        spawnInstallationObj.AddComponent<InstallationController>()._installationData = kneader;
        spawnInstallationObj.AddComponent<InstallationSpawnController>();
    }
    
    public void SpawnOven()
    {
        GameObject spawnInstallationObj = PoolManager.instacne.SpawnFromPool(installationObj);
        spawnInstallationObj.transform.position = new Vector3(0f, 0f, 0f);
        spawnInstallationObj.AddComponent<InstallationController>()._installationData = oven;
    }

    public void Spawn(GameObject spawningInstallationObj, string spawnObjName)
    {
        GameObject curSpawnObj = PoolManager.instacne.SpawnFromPool(ingredientObj);
        curSpawnObj.transform.position = spawningInstallationObj.transform.position + ((spawningInstallationObj.GetComponent<InstallationController>()._installationData.destinationInstallation.transform.position - spawningInstallationObj.transform.position).normalized);

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

        if (spawningInstallationObj.GetComponent<InstallationController>() != null && curSpawnObj.GetComponent<IngredientController>()._ingredientData.canMove)
        {
            MovementController movementController = curSpawnObj.AddComponent<MovementController>();
            movementController.Move(spawningInstallationObj.GetComponent<InstallationController>()._installationData.destinationInstallation);
        }

    }
}
