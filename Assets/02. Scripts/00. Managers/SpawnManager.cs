using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [Header("StandardPrefab")]
    public GameObject installationObj;
    public GameObject ingredientObj;


    public static SpawnManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnInstallaion(MachineSO installationData)
    {
        GameObject spawnInstallationObj = PoolManager.instacne.SpawnFromPool(installationObj);
        spawnInstallationObj.transform.position = new Vector3(0f, 0f, 0f);
        
        InstallationController controller = spawnInstallationObj.GetComponent<InstallationController>();
        
        controller._installationData = installationData;
    }

    public void SpawnIngredient(GameObject spawningInstallationObj, InstallationController installationController, ItemSO data)
    {
        UpdateObjTag(data.tag);
        
        GameObject curSpawnObj = PoolManager.instacne.SpawnFromPool(ingredientObj);
        IngredientController controller = curSpawnObj.GetComponent<IngredientController>();
        
        controller.itemData = data;
        controller.destination = installationController.destinationObj;
        
        curSpawnObj.transform.position = spawningInstallationObj.transform.position +
                                         ((installationController.destinationObj.transform.position -
                                           spawningInstallationObj.transform.position).normalized);
        
        UpdateObjTag(data.tag);
    }

    public void SpawnObjPositionSet(GameObject spawnObj, GameObject destinationObj)
    {
        spawnObj.transform.position = spawnObj.transform.position +
                                      ((destinationObj.transform.position - spawnObj.transform.position)
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
