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

    private int randomNum;
    
    //퀘스트 정보
    private int curQuestCount;
    private int maxQuestCount;
    
    //아이템 모으기
    private int makeQuestItemID;
    
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
        QuestIDSet();

        randomNum = 0; //실험용
        var (detail, reward, count) = GameManager.instance.questManager.QuestText(randomNum);

        questDetail.text = $"{GameManager.instance.dataManager.foodSub[makeQuestItemID].itemName} {detail}";
        questReward.text = reward;
        
        int.TryParse(count, out maxQuestCount);

        int randomIndex = Random.Range(0, GameManager.instance.dataManager.playerData.recipeIndex.Count);
        makeQuestItemID = GameManager.instance.dataManager.playerData.recipeIndex[randomIndex];

        CurQuestCountUpdate();
        
        questType = GameManager.instance.questManager.data_Quest[randomNum][QuestManager.QuestType.Category.ToString()]
            .ToString();
        
        
    }

    private void CurQuestCountUpdate()
    {
        questCount.text = $"( {curQuestCount} / {maxQuestCount} )";
    }

    private void ResetQuest()
    {
        randomNum = -1;
        curQuestCount = 0;
        maxQuestCount = 999;
        questType = "";
        
        questDetail.text = "???";
        questReward.text = "(?? / ??)";
        questCount.text = "???";
    }

    private void QuestIDSet()
    {
        randomNum = Random.Range(0, GameManager.instance.questManager.data_Quest.Count + 1);
    }

    private void MakeQuestUpdate(ItemSO obj)
    {
        Debug.Log(makeQuestItemID + 1000);
        Debug.Log(obj.id);
        if(makeQuestItemID + 1000 != obj.id)
            return;

        curQuestCount++;
        CurQuestCountUpdate();

        if (curQuestCount >= maxQuestCount)
        {
            ResetQuest();
            InitSet();
        }
    }
}
