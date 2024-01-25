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
        
        InstallationController controller = spawnInstallationObj.GetComponent<InstallationController>();
        
        controller._installationData = installationData;
    }

    public void SpawnIngredient(GameObject spawningInstallationObj, IngredientData data)
    {
        UpdateObjTag(data.tag);
        
        GameObject curSpawnObj = PoolManager.instacne.SpawnFromPool(ingredientObj);
        SpawnObjPositionSet(spawningInstallationObj, curSpawnObj, data);

        IngredientController controller = curSpawnObj.GetComponent<IngredientController>();
        controller._ingredientData = data; 
        
        UpdateObjTag(data.tag);
    }

    public void SpawnObjPositionSet(GameObject installation, GameObject spawnObj, IngredientData data)
    {
        spawnObj.transform.position = installation.transform.position +
                                      ((data.destination.transform.position - installation.transform.position)
                                          .normalized);
    }


    private void UpdateObjTag(string spawnObjTag)
    {
        if (ingredientObj.tag == "Untagged")
        {
            ingredientObj.tag = spawnObjTag;
        }
        else
        {
            ingredientObj.tag = "Untagged";
        }
    }
}
