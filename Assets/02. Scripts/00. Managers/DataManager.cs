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
    public Queue<int> installationSubInt = new Queue<int>();
    public Queue<Vector2> installationsPos = new Queue<Vector2>();
    public int ingredients = 0;
}
public class DataManager : MonoBehaviour  // TODO 추후 데이터 저장 / 로딩 관리하기 위한 매니저. json
{
    public PlayerData playerData = new PlayerData();
    private string path;
    private string jsonName = "PlayerJson";

    public MachineSO[] installationSub;

    private void Start()
    {
        path = Application.persistentDataPath + "/";
        SaveData();

        while (playerData.installations > 0)
        {
            playerData.installations--;
            GameObject curGameObject = GameManager.instance.spawnManager.SpawnInstallaion(installationSub[playerData.installationSubInt.Dequeue()]);
            curGameObject.transform.position = playerData.installationsPos.Dequeue();
        }

    }

    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(playerData);
        File.WriteAllText(path + jsonName, jsonData);
        
        Debug.Log(Application.persistentDataPath + "/" + jsonName);
    }

    public void SaveInstallation(int MachineIndex, Vector2 MachinePos)
    {
        playerData.installations++;
        playerData.installationSubInt.Enqueue(MachineIndex);
        playerData.installationsPos.Enqueue(MachinePos);
    }

    public void LoadData()
    {
        string jsonPath = path + jsonName;
        
        if (File.Exists(jsonPath))
        {
            string jsonData = File.ReadAllText(jsonPath);
            playerData = JsonUtility.FromJson<PlayerData>(jsonData);
        }
        else
        {
            Debug.LogWarning("No saved data found.");
        }
    }
}
