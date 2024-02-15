using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Dictionary<string, object>> data_Quest = null;
    
    public enum QuestType
    {
        ID,
        Category,
        Title,
        Detail,
        Reward,
        Count
    }
    
    public enum CategoryType
    {
        make,
        collect
    }

    private void Awake()
    {
        data_Quest = CSVReader.Read("QuestCSV - Quest");
    }

    public (string, string, string) QuestText(int questNum)
    {
        string detail = data_Quest[questNum][QuestType.Detail.ToString()].ToString();
        string reward = data_Quest[questNum][QuestType.Reward.ToString()].ToString();
        string count = data_Quest[questNum][QuestType.Count.ToString()].ToString();

        return (detail, reward, count);
    }
}
