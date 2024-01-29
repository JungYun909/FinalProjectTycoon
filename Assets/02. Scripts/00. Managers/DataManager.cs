using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int level = 1;
    public int money = 0;
    public int installations = 0;
    public List<int> installationSubInt = new List<int>();
    public List<Vector2> installationsPos = new List<Vector2>();
    public int ingredients = 0;
}
public class DataManager : MonoBehaviour  // TODO 추후 데이터 저장 / 로딩 관리하기 위한 매니저. json
{
    public PlayerData playerData = new PlayerData();
    private string path;
    private string jsonName = "PlayerJson";

    public MachineSO[] installationSub;
    public GameObject[] curObject;

    public List<GameObject> curInstallations;

    private void Start()
    {
        path = Application.persistentDataPath + "/";
        
        if (!File.Exists(path + jsonName))
        {
            SaveData();
        }
        
        LoadData();
        
        for (int i = 0; i < playerData.installations; i++)
        {
            GameObject curObj = GameManager.instance.poolManager.SpawnFromPool(curObject[0]);
            InstallationController controller = curObj.GetComponent<InstallationController>();
            controller._installationData = installationSub[playerData.installationSubInt[i]];
            curObj.transform.position = playerData.installationsPos[i];
        }
    }

    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(playerData);
        File.WriteAllText(path + jsonName, jsonData);
    }

    public void SaveInstallation(int MachineIndex, Vector2 MachinePos)
    {
        playerData.installations++;
        playerData.installationSubInt.Add(MachineIndex);
        playerData.installationsPos.Add(MachinePos);
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
    }

    public void LoadData()
    {
        string jsonData = File.ReadAllText(path + jsonName);
        playerData = JsonUtility.FromJson<PlayerData>(jsonData);
    }
}
