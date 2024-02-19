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
    public float time = 0f;
    public int day = 1;
    public int debt = 10000;
    public int fame = 0;
    
    public List<int> installationSubInt = new List<int>();
    public List<Vector2> installationsPos = new List<Vector2>();
    public List<int> recipeIndex = new List<int>();
}
public class DataManager : MonoBehaviour  // TODO 추후 데이터 저장 / 로딩 관리하기 위한 매니저. json
{
    public PlayerData playerData = new PlayerData();
    private string path;
    private string jsonName = "PlayerJson";

    public MachineSO[] installationSub;
    public ItemSO[] ingredientSub;
    public ItemSO[] foodSub;
    
    public GameObject[] curObject;

    [Header("EssentialInstallation")]
    public List<GameObject> curInstallations; //판매씬에 배치된 진열대
    public GameObject counter; // 카운터 등록
    public GameObject entrance;
    public GameObject kitchenDoor;
    
    public event Action OnSaveEvent;
    public event Action OnLoadEvent; 
    public event Action<Vector3> PosUpdateEvent;

    private void Start()
    {
        InitSet(SceneType.MainScene);
    }
    public void Initialize()
    {
        GameManager.instance.sceneManager.sceneInfo += InitSet;
    }

    private void Awake()
    {
        path = Application.persistentDataPath + "/";
        
        if (!File.Exists(path + jsonName))
        {
            ResetData();
        }
        
        LoadData();
    }

    public void InitSet(SceneType sceneType)
    {
        if(sceneType != SceneType.MainScene)
            return;
        
        counter = GameObject.Find("CounterObj");
        entrance = GameObject.Find("Entrance");
        kitchenDoor = GameObject.Find("KitchenDoor");
        
        GameManager.instance.recipeManager.OnCompareRecipe += DiscoverRecipe;
        // GameManager.instance.recipeManager.OnCompareRecipe += DiscoverRecipe;
        //
        // path = Application.persistentDataPath + "/";
        //
        // if (!File.Exists(path + jsonName))
        // {
        //     ResetData();
        // }
        //
        // LoadData();

        LoadInstallation();
    }

    public void ResetData()
    {
        playerData.shopName = "";
        playerData.level = 1;
        playerData.money = 1000;
        playerData.warningCount = 0;
        playerData.time = 0f;
        playerData.day = 1;
        playerData.debt = 10000;
        playerData.fame = 0;
        playerData.installationSubInt.Clear();
        playerData.installationsPos.Clear();
        
        SaveData();
    }

    private void LoadInstallation()
    {
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != SceneType.MainScene.ToString())
            return;
        
        for (int i = 0; i < playerData.installationSubInt.Count; i++)
        {
            GameObject curObj = GameManager.instance.poolManager.SpawnFromPool(curObject[0]);
            InstallationController controller = curObj.GetComponent<InstallationController>();
            controller._installationData = installationSub[playerData.installationSubInt[i]];
            curObj.transform.position = playerData.installationsPos[i];
            curInstallations.Add(curObj);
        }
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

    public void SaveInstallation(GameObject obj)
    {
        InstallationController controller = obj.GetComponent<InstallationController>();

        foreach (var installation in curInstallations)
        {
            if (installation == obj)
            {
                playerData.installationSubInt.Remove(controller._installationData.id - 1);
                playerData.installationsPos.Remove(obj.transform.position);
                curInstallations.Remove(installation);
                return;
            }
        }
        
        playerData.installationSubInt.Add(controller._installationData.id - 1);
        playerData.installationsPos.Add(obj.transform.position);
        curInstallations.Add(obj);
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
        //배치위치받아오는시점
        PosUpdateEvent?.Invoke(curObj.transform.position);
    }

    public void LoadData()
    {
        string jsonData = File.ReadAllText(path + jsonName);
        playerData = JsonUtility.FromJson<PlayerData>(jsonData);
        OnLoadEvent?.Invoke();
    }

    public void SaveInventoryData(InventoryData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/inventory" + data.inventoryID + ".json", json);
    }

    public InventoryData LoadInventoryData(int inventoryID)
    {
        string path = Application.persistentDataPath + "/inventory" + inventoryID + ".json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            InventoryData data = JsonUtility.FromJson<InventoryData>(json);
            // 여기서 data.items 리스트를 순회하면서 각 ItemData의 itemID를 사용해 ItemSO 객체를 검색 및 복원
            return data;
        }
        return null;
    }

    public void SaveDestinationData(DestinationData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/DestinationInfo" + data.controllerID + ".json", json);
    }


    public DestinationData LoadDestinationData(int controllerID)
    {
        string path = Application.persistentDataPath + "/DestinationInfo" + controllerID + ".json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            DestinationData data = JsonUtility.FromJson<DestinationData>(json);
            return data;
        }
        else
        {
            return null;
        }
    }

    public bool IsFileExist(int inventoryID)
    {
        string path = Application.persistentDataPath + "/inventory" + inventoryID + ".json";
        return File.Exists(path);
    }
}
