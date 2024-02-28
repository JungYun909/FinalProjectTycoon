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
    
    private Queue<(string, int)> rankList = new Queue<(string, int)>();
    
    private void Start()
    {
        GameManager.instance.firebaseDatabaseManager.InitSet();
        GameManager.instance.firebaseAuthManager.CreatIDEvent += SaveData;
    }

    private void SaveData(string userID)
    {
        GameManager.instance.firebaseDatabaseManager.SaveData(userID);
    }

    public void OnRankBoard()
    {
        LoadMoneyRanking();
        
        for (int i = 0; i < rankList.Count; i++)
        {
            var (id, money) = rankList.Dequeue(); 
            
            FBRankSlotController curSlot = Instantiate(rankSlotPrefab, slots);
            curSlot.playerRank.text = i.ToString();
            curSlot.playerID.text = id;
            curSlot.playerMoney.text = money.ToString();
        }
        
        
    }

    private void LoadMoneyRanking()
    {
        FirebaseDataType title = FirebaseDataType.RankData;
        string type = "money";
        rankList = GameManager.instance.firebaseDatabaseManager.LoadRankData(FirebaseDataType.RankData, type, rankValue);
    }
    
    private void LoadPlayerMoneyRanking()
    {
        FirebaseDataType title = FirebaseDataType.RankData;
        string playerID = GameManager.instance.firebaseDatabaseManager._rankData.userId;
        int playerMoney = GameManager.instance.firebaseDatabaseManager._rankData.money;
        string type = "money";
        FBRankSlotController curSlot = Instantiate(rankSlotPrefab, slots);
        curSlot.playerRank.text =
            GameManager.instance.firebaseDatabaseManager.LoadPlayerRankData(FirebaseDataType.RankData, type, playerID).ToString();
        curSlot.playerID.text = playerID;
        curSlot.playerMoney.text = playerMoney.ToString();
    }
}
