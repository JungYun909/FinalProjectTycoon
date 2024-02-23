using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;


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

    public bool deliveryClear = false;
    public bool deliveryStart = false;

    public List<int> installationSubInt = new List<int>();
    public List<Vector2> installationsPos = new List<Vector2>();
    
    public List<int> recipeIndex = new List<int>();
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
    
    public MachineDatabaseSO installationData;
    public ItemSO[] ingredientSub;
    public ItemSO[] foodSub;
    
    public GameObject[] curObject;

    [Header("EssentialInstallation")]
    public List<GameObject> curInstallations; //판매씬에 배치된 진열대
    public GameObject counter; // 카운터 등록
    public GameObject entrance;
    public GameObject kitchenDoor;
    public GameObject tutoPrefab;
    
    public event Action OnSaveEvent;
    public event Action OnLoadEvent; 
    public event Action<Vector3> PosUpdateEvent;

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
        playerData.deliveryClear = false;
        playerData.deliveryStart = false;

        playerTimeData.time = 0;

        playerTimeData.deliveryMin = 10;
        playerTimeData.deliverySec = 0;
    }

    private void LoadInstallation()
    {
        for (int i = 0; i < playerData.installationSubInt.Count; i++)
        {
            GameObject curObj = GameManager.instance.poolManager.SpawnFromPool(curObject[0]);
            InstallationController controller = curObj.GetComponent<InstallationController>();
            controller._installationData = installationData.GetItemByID(playerData.installationSubInt[i]);
            curObj.transform.position = playerData.installationsPos[i];
            curInstallations.Add(curObj);
        }
        
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

    public void SaveInventoryData(List<InventoryData> allInventories)
    {
        InventoryWrapper wrapper= new InventoryWrapper { allInventories = allInventories };
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(Application.persistentDataPath + "/AllInventories.json", json);
    }
    

    public List<InventoryData> LoadAllInventories()
    {
        string path = Application.persistentDataPath + "/AllInventories.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            var inventoryWrapper = JsonUtility.FromJson<InventoryWrapper>(json);
            return inventoryWrapper.allInventories;
        }
        return new List<InventoryData>();
    }


    public void SaveAllDestinationData(List<DestinationData> allDestinations)
    {
        DestinationWrapper wrapper = new DestinationWrapper { destinations = allDestinations };
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(Application.persistentDataPath + "/Destinations.json", json);
    }

    public List<DestinationData> LoadAllDestinationData()
    {
        string path = Application.persistentDataPath + "/Destinations.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            var destinationWrapper = JsonUtility.FromJson<DestinationWrapper>(json);
            return destinationWrapper.destinations;
        }
        return new List<DestinationData>();
    }
}
