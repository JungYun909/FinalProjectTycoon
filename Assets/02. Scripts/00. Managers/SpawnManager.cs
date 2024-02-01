using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    [Header("StandardPrefab")]
    public GameObject installationObj;
    public GameObject ingredientObj;
    public GameObject npcObj;

    [Header("NPCSpawn")]
    public GameObject NPCSpawnObj;
    public int curNpcCount;
    public List<NpcSO> npcDataList = new List<NpcSO>();
    

    public GameObject SpawnInstallaion(MachineSO installationData)
    {
        GameObject spawnInstallationObj = GameManager.instance.poolManager.SpawnFromPool(installationObj);
        spawnInstallationObj.transform.position =
            new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0f);
        
        InstallationController controller = spawnInstallationObj.GetComponent<InstallationController>();
        controller._installationData = installationData;
        controller.InitSetting();
        GameManager.instance.dataManager.SaveInstallation(spawnInstallationObj);//TODO 데이터 매니저의 설치물 리스트를 통해 갱신시킨다
        GameManager.instance.dataManager.SaveData();

        return spawnInstallationObj;
    }

    public void SpawnIngredient(GameObject spawningInstallationObj, GameObject destinationObj, ItemSO data)
    {
        UpdateObjTag(data.tag);
        
        GameObject curSpawnObj = GameManager.instance.poolManager.SpawnFromPool(ingredientObj);
        IngredientController controller = curSpawnObj.GetComponent<IngredientController>();
        
        controller.itemData = data;
        controller.destination = destinationObj;
        controller.InitSet();
        
        SpawnPositionSet(spawningInstallationObj, destinationObj, curSpawnObj);
        
        UpdateObjTag(data.tag);
    }


    public void SpawnPositionSet(GameObject spawnObj, GameObject destinationObj,GameObject curObj)
    {
        curObj.transform.position = spawnObj.transform.position +
                                    ((destinationObj.transform.position -
                                      spawnObj.transform.position).normalized);
    }

    public void SpawnNPC()
    {
        GameObject curNPC = GameManager.instance.poolManager.SpawnFromPool(npcObj);
        curNpcCount++;
        curNPC.transform.position = NPCSpawnObj.transform.position;
        NPCController npcData = curNPC.GetComponent<NPCController>();
        npcData.curNPCData = npcDataList[Random.Range(0, npcDataList.Count)];
        
        npcData.InitSetting();
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
