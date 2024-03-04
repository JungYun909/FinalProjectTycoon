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
        GameManager.instance.firebaseDatabaseManager.InitSet();
        GameManager.instance.firebaseAuthManager.CreatIDEvent += SaveData;
    }

    private void SaveData()
    {
        GameManager.instance.firebaseDatabaseManager.SaveData();
    }

    public void OnRankBoard()
    {
        GameManager.instance.firebaseDatabaseManager.OnRoadRankData += LoadMoneyRanking;
        GameManager.instance.firebaseDatabaseManager.LoadRankData(FirebaseDataType.RankData, "money", rankValue);

        //LoadPlayerMoneyRanking();
    }

    private void LoadMoneyRanking(Queue<(string, int)> curRank)
    {
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
        GameManager.instance.firebaseDatabaseManager.OnRoadRankData -= LoadMoneyRanking;
    }
    
    private void LoadPlayerMoneyRanking()
    {
        FirebaseDataType title = FirebaseDataType.RankData;
        string playerID = GameManager.instance.firebaseDatabaseManager._rankData.userID;
        int playerMoney = GameManager.instance.firebaseDatabaseManager._rankData.money;
        string type = "money";
        FBRankSlotController curSlot = Instantiate(rankSlotPrefab, slots);
        curSlot.playerRank.text =
            GameManager.instance.firebaseDatabaseManager.LoadPlayerRankData(FirebaseDataType.RankData, type, playerID).ToString();
        curSlot.playerID.text = playerID;
        curSlot.playerMoney.text = playerMoney.ToString();
    }

    public void AAA()
    {
        GameManager.instance.firebaseDatabaseManager.AASaveData();
    }
}
