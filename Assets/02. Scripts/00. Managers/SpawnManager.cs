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
    

    private void Start()
    {
        //StartCoroutine(SpawnNPC());
    }

    private void Update()
    {
        // if(GameManager.instance.statManager.curNpc < GameManager.instance.statManager.maxNpc)
        //     StopCoroutine(SpawnNPC());
    }

    public void SpawnInstallaion(MachineSO installationData)
    {
        GameObject spawnInstallationObj = GameManager.instance.poolManager.SpawnFromPool(installationObj);
        spawnInstallationObj.transform.position = new Vector3(0f, 0f, 0f);
        
        InstallationController controller = spawnInstallationObj.GetComponent<InstallationController>();
        
        controller._installationData = installationData;
        
        GameManager.instance.dataManager.SaveInstallation(installationData.id - 1, spawnInstallationObj.transform.position);
        GameManager.instance.dataManager.SaveData();
    }

    public void SpawnIngredient(GameObject spawningInstallationObj, GameObject destinationObj, ItemSO data)
    {
        UpdateObjTag(data.tag);
        
        GameObject curSpawnObj = GameManager.instance.poolManager.SpawnFromPool(ingredientObj);
        IngredientController controller = curSpawnObj.GetComponent<IngredientController>();
        
        controller.itemData = data;
        controller.destination = destinationObj;
        
        curSpawnObj.transform.position = spawningInstallationObj.transform.position +
                                         ((destinationObj.transform.position -
                                           spawningInstallationObj.transform.position).normalized);

        GameManager.instance.dataManager.playerData.ingredients++;
        GameManager.instance.dataManager.SaveData();
        UpdateObjTag(data.tag);
    }

    IEnumerator SpawnNPC()
    {
        while (true)
        {
            float visitProbability = GameManager.instance.statManager.shopFame * 0.1f;
        
            int rand = UnityEngine.Random.Range(1, 100);

            if (rand <= visitProbability)
            {
                GameObject curNPC =  GameManager.instance.poolManager.SpawnFromPool(npcObj);
                curNPC.transform.position = door.transform.position;
                GameManager.instance.statManager.maxNpc += 1;
            }
            
            yield return new WaitForSeconds(6 - (GameManager.instance.statManager.shopLevel / 20));
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
