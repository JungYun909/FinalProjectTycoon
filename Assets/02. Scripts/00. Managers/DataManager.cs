using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class PlayerData
{
    public string shopName = "";
    public int level = 1;
    public int money = 0;
    public int warningCount = 0;
    public int day = 1;
    public int debt = 5000;
    public int fame = 0;

    public int questNum = -1;
    public int questCount = 0;
    public int makeQuestItemID = 0;

    public int tutoNum = -1;
    public bool tutoClear = false;

    public bool deliveryClear = false;
    public bool deliveryStart = false;

    public List<int> installationSubInt = new List<int>();
    public List<Vector2> installationsPos = new List<Vector2>();
    
    public List<int> recipeIndex = new List<int>();

    public List<int> inventoryIDs = new List<int>();
    public List<int> destinationIDs = new List<int>();


    public int totalGoldEarned = 0;
    public int goldEarnedToday = 0;
    public int goldSpentToday = 0;
    public int exp = 0;

    public bool happilyEnded = false;
}

public class PlayerTimeData
{
    public float time = 0f;

    public float deliverySec = 0f;
    public int deliveryMin = 10;
}
public class DataManager : MonoBehaviour  // TODO 추후 데이터 저장 / 로딩 관리하기 위한 매니저. json
{
    public PlayerData playerData = new PlayerData();
    public PlayerTimeData playerTimeData = new PlayerTimeData();
    
    private string path;
    private string jsonName = "PlayerJson";
    private string timeJsonName = "PlayerTimeJson";
    
    public MachineDatabaseSO installationDatas;
    public NpcDatabaseSO npcDatas;
    public ItemSO[] ingredientSub;
    public ItemSO[] foodSub;
    
    public GameObject[] curObject;

    [Header("EssentialInstallation")]
    public List<GameObject> curInstallations;
    public GameObject counter; // 카운터 등록
    public GameObject entrance;
    public GameObject kitchenDoor;
    public GameObject tutoPrefab;
    
    public event Action OnSaveEvent;
    public event Action OnLoadEvent;
    public event Action LoadInventoryID;
    public event Action<Vector3> PosUpdateEvent;

    public bool isLoadingInstallationDone = false;

    public bool isClearTuto = false;
    
    public void InitSet()
    {
        path = Application.persistentDataPath + "/";
         
        if (!File.Exists(path + jsonName))
        {
            ResetData();
            SaveData();
            SaveTimeData();
        }
        
        LoadData();
    }
    
    public void LoadInstallationData()
    {
        counter = GameObject.Find("CounterObj");
        entrance = GameObject.Find("Entrance");
        kitchenDoor = GameObject.Find("KitchenDoor");
        GameManager.instance.destinationManager.RegisterDestinationID(kitchenDoor.GetComponent<InstallationController>());
        
        GameManager.instance.recipeManager.OnCompareRecipe += DiscoverRecipe;

        LoadInstallation();
    }

    public void ResetData()
    {
        playerData.shopName = "";
        playerData.level = 1;
        playerData.money = 1000;
        playerData.warningCount = 0;
        playerData.day = 1;
        playerData.debt = 5000;
        playerData.fame = 0;
        playerData.questNum = -1;
        playerData.questCount = 0;
        playerData.makeQuestItemID = 0;
        playerData.installationSubInt.Clear();
        playerData.installationsPos.Clear();
        playerData.inventoryIDs.Clear();
        playerData.destinationIDs.Clear();
        playerData.deliveryClear = false;
        playerData.deliveryStart = false;
        
        curInstallations.Clear();

        playerTimeData.time = 0;

        playerTimeData.deliveryMin = 10;
        playerTimeData.deliverySec = 0;

        playerData.totalGoldEarned = 0;
        playerData.goldEarnedToday = 0;
        playerData.goldSpentToday = 0;
        playerData.exp = 0;

        playerData.happilyEnded = false;
    }

    private void LoadInstallation()
    {
        for (int i = 0; i < playerData.installationSubInt.Count; i++)
        {
            GameObject curObj = GameManager.instance.poolManager.SpawnFromPool(curObject[0]);
            InstallationController controller = curObj.GetComponent<InstallationController>();
            controller._installationData = installationDatas.GetItemByID(playerData.installationSubInt[i]);
            controller.InitializeDestinationSetting(playerData.destinationIDs[i]);
            curObj.transform.position = playerData.installationsPos[i];
            curInstallations.Add(curObj);
            if(playerData.inventoryIDs[i] != -1)
            {
                controller.inventory.InitializeInventory(playerData.inventoryIDs[i]);
            }
        }
        isLoadingInstallationDone = true;
        SaveData();
    }

    private void DiscoverRecipe(int index)
    {
        if (!playerData.recipeIndex.Contains(index))
        {
            playerData.recipeIndex.Add(index);
            SaveData();
        }
    }

    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(playerData);
        File.WriteAllText(path + jsonName, jsonData);
        
        OnSaveEvent?.Invoke();
    }

    public void SaveTimeData()
    {
        string timeJsonData = JsonUtility.ToJson(playerTimeData);
        File.WriteAllText(path + timeJsonName, timeJsonData);
    }

    public void LoadData()
    {
        string jsonData = File.ReadAllText(path + jsonName);
        playerData = JsonUtility.FromJson<PlayerData>(jsonData);
        
        string timeJsonData = File.ReadAllText(path + timeJsonName);
        playerTimeData = JsonUtility.FromJson<PlayerTimeData>(timeJsonData);
        OnLoadEvent?.Invoke();
    }
    
    public void SaveInstallation(GameObject obj)
    {
        InstallationController controller = obj.GetComponent<InstallationController>();
        playerData.installationSubInt.Add(controller._installationData.id);
        playerData.installationsPos.Add(obj.transform.position);
        int controllerID = GameManager.instance.destinationManager.RegisterDestinationID(controller);
        controller.destinationID = controllerID;
        playerData.destinationIDs.Add(controllerID);

        if (controller.inventory.gameObject.activeSelf)
        {
            int inventoryID = GameManager.instance.inventoryManager.RegisterInventory(controller.inventory);
            controller.inventory.inventoryID = inventoryID;
            playerData.inventoryIDs.Add(inventoryID);
        }
        else
        {
            playerData.inventoryIDs.Add(-1);
        }
        curInstallations.Add(obj);
        
        SaveData();
    }

    public void RemoveInstallationData(GameObject obj)
    {
        InstallationController controller = obj.GetComponent<InstallationController>();

        for (int i = 0; i < curInstallations.Count; i++)
        {
            if (curInstallations[i] == obj)
            {
                playerData.installationSubInt.RemoveAt(i);
                playerData.installationsPos.RemoveAt(i);
                playerData.inventoryIDs.RemoveAt(i);
                playerData.destinationIDs.RemoveAt(i);
                curInstallations.RemoveAt(i);
                SaveData();
                return;
            }
        }
    }

    public void PosUpdate(GameObject curObj)
    {
        for (int i = 0; i < curInstallations.Count; i++)
        {
            if (curInstallations[i] == curObj)
            {
                playerData.installationsPos[i] = curObj.transform.position;
            }
        }
        SaveData();
        PosUpdateEvent?.Invoke(curObj.transform.position);
    }

    public void SaveInventoryData(int nextInventoryID, List<InventoryData> allInventories)
    {
        InventoryWrapper wrapper = new InventoryWrapper
        {
            nextInventoryID = nextInventoryID,
            allInventories = allInventories 
        };
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(Application.persistentDataPath + "/AllInventories.json", json);
    }


    public InventoryWrapper LoadAllInventories()
    {
        string path = Application.persistentDataPath + "/AllInventories.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            InventoryWrapper inventoryWrapper = JsonUtility.FromJson<InventoryWrapper>(json);
            return inventoryWrapper;
        }
        return new InventoryWrapper { nextInventoryID = 1001, allInventories = new List<InventoryData>() } ;
    }


    public void SaveAllDestinationData(int nextDestinationID, List<DestinationData> allDestinations)
    {
        DestinationWrapper wrapper = new DestinationWrapper
        {
            nextDestinationID = nextDestinationID,
            destinations = allDestinations 
        };
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(Application.persistentDataPath + "/Destinations.json", json);
    }

    public DestinationWrapper LoadAllDestinationData()
    {
        string path = Application.persistentDataPath + "/Destinations.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            var destinationWrapper = JsonUtility.FromJson<DestinationWrapper>(json);
            return destinationWrapper;
        }
        return new DestinationWrapper { nextDestinationID = 2, destinations = new List<DestinationData>() };
    }

    public void ResetInventoryAndDestinationData()
    {
        GameManager.instance.inventoryManager.allInventories.Clear();
        GameManager.instance.inventoryManager.inventories.Clear();
        GameManager.instance.inventoryManager.nextInventoryID = 1001;
        SaveInventoryData(1001, GameManager.instance.inventoryManager.allInventories);
        GameManager.instance.destinationManager.destinationDictionary.Clear();
        GameManager.instance.destinationManager.destinationInfo.Clear();
        GameManager.instance.destinationManager.destinationControllerID = 2;
        SaveAllDestinationData(2, GameManager.instance.destinationManager.destinationInfo);
    }
}
