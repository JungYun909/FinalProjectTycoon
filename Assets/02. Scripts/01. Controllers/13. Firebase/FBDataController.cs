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

    private int chargePerDay;

    private Queue<(string, int)> rankList;
    
    private void Start()
    {
        GameManager.instance.firebaseDatabaseManager.InitSet();
        GameManager.instance.firebaseAuthManager.CreatIDEvent += SaveData;
        GameManager.instance.statManager.OnMoneyChange += EarnedPerDay;
        GameManager.instance.statManager.onDateChanged += ResetEarnedPerDay;
    }

    private void ResetEarnedPerDay()
    {
        if (GameManager.instance.firebaseDatabaseManager._rankData.earnedPerDay < chargePerDay)
        {
            GameManager.instance.firebaseDatabaseManager._rankData.earnedPerDay = chargePerDay;
        }

        chargePerDay = 0;
        SaveData();
    }

    private void EarnedPerDay(int money)
    {
        chargePerDay += money;
    }

    private void SaveData()
    {
        GameManager.instance.firebaseDatabaseManager.SaveData();
    }

    public void OnRankBoard()
    {
        GameManager.instance.firebaseDatabaseManager.OnRoadRankData += LoadMoneyRanking;
        GameManager.instance.firebaseDatabaseManager.LoadRankData(FirebaseDataType.RankData, "earnedPerDay", rankValue);
    }

    private void LoadMoneyRanking(Queue<(string, int)> curRank)
    {
        foreach (GameObject child in slots)
        {
            Destroy(child);
        }
        
        rankList = new Queue<(string, int)>(curRank);

        int count = rankList.Count;
        for (int i = 0; i < count; i++)
        {
            Debug.Log(rankList.Count);

            var (id, money) = rankList.Dequeue(); 
            FBRankSlotController curSlot = Instantiate(rankSlotPrefab, slots);
            curSlot.playerRank.text = (i + 1).ToString();
            curSlot.playerID.text = id;
            curSlot.playerMoney.text = money.ToString();
        }
        
        LoadPlayerMoneyRanking();
        GameManager.instance.firebaseDatabaseManager.OnRoadRankData -= LoadMoneyRanking;
    }
    
    private void LoadPlayerMoneyRanking()
    {
        FBRankSlotController playerSlot = Instantiate(rankSlotPrefab, slots);
        Debug.Log(GameManager.instance.firebaseDatabaseManager._rankData.userName);
        Debug.Log(GameManager.instance.firebaseDatabaseManager._rankData.earnedPerDay.ToString());
        playerSlot.playerRank.text =
            GameManager.instance.firebaseDatabaseManager.LoadPlayerRankData(FirebaseDataType.RankData, "earnedPerDay").ToString();
        playerSlot.playerID.text = GameManager.instance.firebaseDatabaseManager._rankData.userName;
        playerSlot.playerMoney.text = GameManager.instance.firebaseDatabaseManager._rankData.earnedPerDay.ToString();
    }

    public void AAA()
    {
        GameManager.instance.firebaseDatabaseManager.AASaveData();
    }
}
