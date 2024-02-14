using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private List<Dictionary<string, object>> data_Quest = null;

    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI questDetail;
    public TextMeshProUGUI questReward;
    
    private enum QuestType
    {
        ID,
        Category,
        Title,
        Detail,
        Reward,
        NeedID,
        InstallID,
        FoodID
    }

    private void Awake()
    {
        data_Quest = CSVReader.Read("QuestCSV - Quest");
    }

    public void StartQuest(int questNum)
    {
        questTitle.text = data_Quest[questNum][QuestType.Title.ToString()].ToString();
        questDetail.text = data_Quest[questNum][QuestType.Detail.ToString()].ToString();
        questReward.text = data_Quest[questNum][QuestType.Reward.ToString()].ToString();
    }
}
