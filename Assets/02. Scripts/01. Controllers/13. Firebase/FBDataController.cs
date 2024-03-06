using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FBDataController : MonoBehaviour
{
    [SerializeField] private int rankValue;
    [SerializeField] private Transform slots;
    [SerializeField] private FBRankSlotController rankSlotPrefab;


    private Queue<(string, int)> rankList;
    
    private void Start()
    {
        if(GameManager.instance.firebaseDatabaseManager._reference == null)
            GameManager.instance.firebaseDatabaseManager.InitSet();
        
        GameManager.instance.firebaseAuthManager.CreatIDEvent += SaveData;
        GameManager.instance.dataManager.OnMoneyRankUpdate += SaveMoneyData;
    }

    private void OnDisable()
    {
        GameManager.instance.firebaseAuthManager.CreatIDEvent -= SaveData;
        GameManager.instance.dataManager.OnMoneyRankUpdate -= SaveMoneyData;
    }

    private void SaveMoneyData(int money)
    {
        GameManager.instance.firebaseDatabaseManager.SaveMoneyDate(money);
    }

    private void SaveData()
    {
        GameManager.instance.firebaseDatabaseManager.SaveData();
    }

    public void OnRankBoard()
    {
        GameManager.instance.firebaseDatabaseManager.OnRoadRankData += LoadMoneyRanking;
        GameManager.instance.firebaseDatabaseManager.OnRoadPlayerRankData += LoadPlayerMoneyRanking;
        GameManager.instance.firebaseDatabaseManager.LoadRankData(FirebaseDataType.RankData, "earnedPerDay", rankValue);
    }

    private void LoadMoneyRanking(Queue<(string, int)> curRank)
    {
        foreach (Transform child in slots)
        {
            Destroy(child.gameObject);
        }
        
        rankList = new Queue<(string, int)>(curRank);

        int count = rankList.Count;
        for (int i = 0; i < count; i++)
        {
            var (id, money) = rankList.Dequeue(); 
            FBRankSlotController curSlot = Instantiate(rankSlotPrefab, slots);
            curSlot.playerRank.text = (i + 1).ToString();
            curSlot.playerID.text = id;
            curSlot.playerMoney.text = money.ToString();
            
            if (i > 0)
            {
                curSlot.convertedColor = new Color(0.2941f, 0.1922f, 0.1765f, 1f);
            }
        }
        
        GameManager.instance.firebaseDatabaseManager.LoadPlayerRankData(FirebaseDataType.RankData, "earnedPerDay");
        GameManager.instance.firebaseDatabaseManager.OnRoadRankData -= LoadMoneyRanking;
    }
    
    private void LoadPlayerMoneyRanking(int rank)
    {
        FBRankSlotController playerSlot = Instantiate(rankSlotPrefab, slots);
        playerSlot.playerRank.text = rank.ToString();
        playerSlot.playerID.text = GameManager.instance.firebaseDatabaseManager._rankData.userName;
        playerSlot.playerMoney.text = GameManager.instance.firebaseDatabaseManager._rankData.earnedPerDay.ToString();
        playerSlot.convertedColor = Color.red;
        GameManager.instance.firebaseDatabaseManager.OnRoadPlayerRankData -= LoadPlayerMoneyRanking;
    }

    public void AAA()
    {
        GameManager.instance.firebaseDatabaseManager.AASaveData();
    }

    public void BBB(int money)
    {
        GameManager.instance.statManager.EarnGold(money);
    }
}
