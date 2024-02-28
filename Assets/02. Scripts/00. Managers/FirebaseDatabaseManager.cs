using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public enum FirebaseDataType
{
    RankData,
    PlayerData
}
public class RankData
{
    public string userId;
    public int money;
}

public class FirebaseDatabaseManager : MonoBehaviour
{
    private DatabaseReference _reference;
    public RankData _rankData;

    public void InitSet()
    {
        _reference = FirebaseDatabase.DefaultInstance.RootReference;
        _rankData = new RankData();
    }

    public void SaveData(string userID)
    {
        _rankData.userId = userID;
        _rankData.money = GameManager.instance.dataManager.playerData.money;

        RankData rankData = _rankData;
        string jsonData = JsonUtility.ToJson(rankData);
        _reference.Child(FirebaseDataType.RankData.ToString()).SetRawJsonValueAsync(jsonData);
    }

    // public RankData LoadData(string userID)
    // {
    //     DatabaseReference curDatabaseReference = _reference.Child(userID);
    //
    //     curDatabaseReference.GetValueAsync().ContinueWithOnMainThread(task =>
    //     {
    //         if (task.IsCanceled || task.IsFaulted)
    //         {
    //             Debug.Log("저장 데이터를 불러오는데 실패하였습니다");
    //             return null;
    //         }
    //
    //         DataSnapshot snapshot = task.Result;
    //
    //         if (!snapshot.Exists)
    //             return null;
    //
    //         string jsonData = snapshot.GetRawJsonValue();
    //         RankData rankData = JsonUtility.FromJson<RankData>(jsonData);
    //
    //         return rankData;
    //     });
    //     
    //     return null;
    // }

    public Queue<(string, int)> LoadRankData(FirebaseDataType title, string type, int maxValue)
    {
        _reference.Child(title.ToString()).OrderByChild(type).LimitToFirst(maxValue).GetValueAsync()
            .ContinueWithOnMainThread(
                task =>
                {
                    if (task.IsCanceled || task.IsFaulted)
                    {
                        Debug.Log("랭킹 탑10 불러오기 실패");
                        return null;
                    }

                    DataSnapshot snapshot = task.Result;
                    Queue<(string, int)> rankList = new Queue<(string, int)>();

                    foreach (var childSnapshot in snapshot.Children)
                    {
                        if (title == FirebaseDataType.RankData)
                            rankList.Enqueue((childSnapshot.Child("userID").Value.ToString(),
                                int.Parse(childSnapshot.Child("money").Value.ToString())));
                    }

                    return rankList;
                });
        return null;
    }
    
    public int LoadPlayerRankData(FirebaseDataType title, string type, string id)
    {
        _reference.Child(title.ToString()).OrderByChild(type).GetValueAsync()
            .ContinueWithOnMainThread(
                task =>
                {
                    int count = 0;

                    if (task.IsCanceled || task.IsFaulted)
                    {
                        Debug.Log("랭킹 불러오기 실패");
                        return count;
                    }

                    DataSnapshot snapshot = task.Result;

                    foreach (var childSnapshot in snapshot.Children)
                    {
                        if (title == FirebaseDataType.RankData)
                        {
                            if (childSnapshot.Child("userId").Value.ToString() != id)
                            {
                                count++;
                                continue;
                            }
                            
                            return count;
                        }
                            
                    }

                    return 0;
                });
        return 0;
    }
}
