using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

public enum FirebaseDataType
{
    RankData,
    PlayerData
}
public class RankData
{
    public string userID;
    public string userName;
    public int earnedPerDay;
}

public class FirebaseDatabaseManager : MonoBehaviour
{
    private DatabaseReference _reference;
    public RankData _rankData;

    public Action<Queue<(string, int)>> OnRoadRankData;
    public Action<int> OnRoadPlayerRankData;

    public void InitSet()
    {
        _reference = FirebaseDatabase.DefaultInstance.RootReference;
        _rankData = new RankData();
    }

    public void SaveData()
    {
        if (GameManager.instance.firebaseAuthManager.userID != null)
        {
            _rankData.userID = GameManager.instance.firebaseAuthManager.userID;
        }
        else
        {
            Debug.Log("notExist");
            return;
        }

        if (GameManager.instance.dataManager.playerData.shopName != "" || _rankData.userID != null)
        {
            _rankData.userName = GameManager.instance.dataManager.playerData.shopName + "#" + _rankData.userID.Substring(0, 2);
        }
        else
        {
            Debug.Log("notExist2");
            return;
        }

        _rankData.earnedPerDay = GameManager.instance.dataManager.playerData.earnedPerDay;
        
        Debug.Log(_rankData.userID);
        Debug.Log(_rankData.userName);

        RankData rankData = new RankData();
        rankData = _rankData;
        string jsonData = JsonUtility.ToJson(rankData);
        _reference.Child(FirebaseDataType.RankData.ToString()).Child(GameManager.instance.firebaseAuthManager.userID).SetRawJsonValueAsync(jsonData);
        Debug.Log("save");
    }

    public void SaveMoneyDate(int money)
    {
        _rankData.earnedPerDay = money;
        Debug.Log(money);
        
        RankData rankData = new RankData();
        rankData = _rankData;
        string jsonData = JsonUtility.ToJson(rankData);
        _reference.Child(FirebaseDataType.RankData.ToString()).Child(GameManager.instance.firebaseAuthManager.userID).SetRawJsonValueAsync(jsonData);
    }
    
    public void AASaveData()
    {
        for (int i = 0; i < 20; i++)
        {
            _rankData.userID = i.ToString();
            _rankData.userName = i.ToString();
            _rankData.earnedPerDay = 1000 * Random.Range(1, 20);
            RankData rankData = new RankData();
            rankData = _rankData;
            string jsonData = JsonUtility.ToJson(rankData);
            _reference.Child(FirebaseDataType.RankData.ToString()).Child(_rankData.userID).SetRawJsonValueAsync(jsonData);
            Debug.Log("save");
        }
    }

    public void LoadRankData(FirebaseDataType title, string type, int maxValue)
    {
        _reference.Child(title.ToString()).OrderByChild(type).LimitToLast(maxValue).GetValueAsync()
            .ContinueWithOnMainThread(
                task =>
                {
                    if (task.IsCanceled || task.IsFaulted)
                    {
                        Debug.Log("랭킹 탑10 불러오기 실패");
                        return;
                    }

                    DataSnapshot snapshot = task.Result;
                    Queue<(string, int)> rankList = new Queue<(string, int)>();

                    for (int i = (int)snapshot.ChildrenCount; i > 0; i--)
                    {
                        DataSnapshot childSnapshot = snapshot.Children.ElementAt(i - 1);
                        
                        if (title == FirebaseDataType.RankData)
                        {
                            rankList.Enqueue((childSnapshot.Child("userName").Value.ToString(),
                                int.Parse(childSnapshot.Child("earnedPerDay").Value.ToString())));
                        }
                    }
                    
                    OnRoadRankData?.Invoke(rankList);
                });
    }
    
    public void LoadPlayerRankData(FirebaseDataType title, string type)
    {
        _reference.Child(title.ToString()).OrderByChild(type).GetValueAsync()
            .ContinueWithOnMainThread(
                task =>
                {
                    if (task.IsCanceled || task.IsFaulted)
                    {
                        Debug.Log("랭킹 불러오기 실패");
                        return;
                    }

                    DataSnapshot snapshot = task.Result;
                    int count = 1;

                    for (int i = (int)snapshot.ChildrenCount; i > 0; i--)
                    {
                        DataSnapshot childSnapshot = snapshot.Children.ElementAt(i - 1);
                        
                        if (title == FirebaseDataType.RankData)
                        {
                            string userName = childSnapshot.Child("userName").Value?.ToString();

                            Debug.Log(userName);
                            Debug.Log(_rankData.userName);

                            if (userName != _rankData.userName)
                            {
                                Debug.Log(_rankData.userID + "와 일치하지 않는 횟수");
                                count++;
                                continue;
                            }

                            Debug.Log(_rankData.userID + "와 일치하는 횟수");
                            break;
                        }
                    }
                    OnRoadPlayerRankData?.Invoke(count);
                });
    }
}
