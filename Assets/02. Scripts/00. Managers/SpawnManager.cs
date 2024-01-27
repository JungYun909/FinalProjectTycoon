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
    public GameObject npcObj;

    [Header("SpawnPosition")]
    public GameObject door;


    public static SpawnManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(SpawnNPC());
    }

    private void Update()
    {
        if(StatManager.instance.curNpc < StatManager.instance.maxNpc)
            StopCoroutine(SpawnNPC());
    }

    public void SpawnInstallaion(MachineSO installationData)
    {
        GameObject spawnInstallationObj = PoolManager.instacne.SpawnFromPool(installationObj);
        spawnInstallationObj.transform.position = new Vector3(0f, 0f, 0f);
        
        InstallationController controller = spawnInstallationObj.GetComponent<InstallationController>();
        
        controller._installationData = installationData;
    }

    public void SpawnIngredient(GameObject spawningInstallationObj, GameObject destinationObj, ItemSO data)
    {
        UpdateObjTag(data.tag);
        
        GameObject curSpawnObj = PoolManager.instacne.SpawnFromPool(ingredientObj);
        IngredientController controller = curSpawnObj.GetComponent<IngredientController>();
        
        controller.itemData = data;
        controller.destination = destinationObj;
        
        curSpawnObj.transform.position = spawningInstallationObj.transform.position +
                                         ((destinationObj.transform.position -
                                           spawningInstallationObj.transform.position).normalized);
        
        UpdateObjTag(data.tag);
    }

    IEnumerator SpawnNPC()
    {
        while (true)
        {
            float visitProbability = StatManager.instance.shopFame * 0.1f;
        
            int rand = UnityEngine.Random.Range(1, 100);

            if (rand <= visitProbability)
            {
                GameObject curNPC =  PoolManager.instacne.SpawnFromPool(npcObj);
                curNPC.transform.position = door.transform.position;
                StatManager.instance.maxNpc += 1;
            }
            
            yield return new WaitForSeconds(6 - (StatManager.instance.shopLevel / 20));
        }
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
