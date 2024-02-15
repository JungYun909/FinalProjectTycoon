using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int level = 1;
    public int money = 0;
    public int warningCount = 0;
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
    
    public event Action OnSaveEvent;
    public event Action<Vector3> PosUpdateEvent;

    public void Initialize()
    {
        GameManager.instance.sceneManager.sceneInfo += InitSet;
    }

    public void InitSet(SceneType sceneType)
    {
        if(sceneType != SceneType.MainScene)
            return;
        
        counter = GameObject.Find("CounterObj");
        entrance = GameObject.Find("Entrance");
        
        GameManager.instance.recipeManager.OnCompareRecipe += DiscoverRecipe;
        
        path = Application.persistentDataPath + "/";
        
        if (!File.Exists(path + jsonName))
        {
            SaveData();
        }
        
        LoadData();

        LoadInstallation();
    }

    public void ResetData()
    {
        playerData.level = 1;
        playerData.money = 0;
        playerData.warningCount = 0;
        playerData.installationsPos.Clear();
        playerData.installationSubInt.Clear();
        
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
    }
}
