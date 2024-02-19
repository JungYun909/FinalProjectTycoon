using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestUIController : UIBase
{
    public TextMeshProUGUI questDetail;
    public TextMeshProUGUI questReward;
    public TextMeshProUGUI questCount;
    
    //퀘스트 정보
    private int maxQuestCount;
    
    private string questType;

    public override void Initialize()
    {
        return;
    }

    public override void UpdateUI()
    {
        return;
    }

    private void OnEnable()
    {
        GameManager.instance.spawnManager.SpawnIngredientEvnet += MakeQuestUpdate;
        GameManager.instance.statManager.onDateChanged += ReStartQuest;
        InitSet();
    }


    private void Start()
    {
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != SceneType.MainScene.ToString())
            return;

        InitSet();
    }

    private void ReStartQuest()
    {
        ResetQuest();
        InitSet();
    }

    private void InitSet()
    {
        if(GameManager.instance.dataManager.playerData.questNum == -1) 
            QuestDataSet();

        var (detail, reward, count) = GameManager.instance.questManager.QuestText(GameManager.instance.dataManager.playerData.questNum);

        questDetail.text = $"{GameManager.instance.dataManager.foodSub[GameManager.instance.dataManager.playerData.makeQuestItemID].itemName} {detail}";
        questReward.text = reward;
        int.TryParse(count, out maxQuestCount);

        CurQuestCountUpdate();
        
        questType = GameManager.instance.questManager.data_Quest[GameManager.instance.dataManager.playerData.questNum][QuestManager.QuestType.Category.ToString()]
            .ToString();
        
        GameManager.instance.dataManager.SaveData();
    }

    private void CurQuestCountUpdate()
    {
        questCount.text = $"( {GameManager.instance.dataManager.playerData.questCount} / {maxQuestCount} )";
    }

    private void ResetQuest()
    {
        GameManager.instance.dataManager.playerData.questNum = -1;
        GameManager.instance.dataManager.playerData.questCount = 0;
        maxQuestCount = 999;
        questType = "";
        
        questDetail.text = "성공!";
        questReward.text = "성공";
        questCount.text = "성공 / 성공";
    }

    private void QuestDataSet()
    {
        GameManager.instance.dataManager.playerData.questNum = Random.Range(0, GameManager.instance.questManager.data_Quest.Count + 1);
        GameManager.instance.dataManager.playerData.questNum = 0; //실험용
        
        if (GameManager.instance.dataManager.playerData.recipeIndex.Count > 0)
        {
            int randomIndex = Random.Range(0, GameManager.instance.dataManager.playerData.recipeIndex.Count);
            GameManager.instance.dataManager.playerData.makeQuestItemID = GameManager.instance.dataManager.playerData.recipeIndex[randomIndex];
        }
        else
        {
            GameManager.instance.dataManager.playerData.makeQuestItemID = 1;
        }

    }

    private void MakeQuestUpdate(ItemSO obj)
    {
        if(GameManager.instance.dataManager.playerData.makeQuestItemID + 1000 != obj.id)
            return;

        GameManager.instance.dataManager.playerData.questCount++;
        CurQuestCountUpdate();

        if (GameManager.instance.dataManager.playerData.questCount >= maxQuestCount)
        {
            ResetQuest();
        }
        
        GameManager.instance.dataManager.SaveData();
    }
}
