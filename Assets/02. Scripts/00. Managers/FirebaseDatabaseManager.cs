using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public void InitSet()
    {
        _reference = FirebaseDatabase.DefaultInstance.RootReference;
        _rankData = new RankData();
    }

    public void SaveData()
    {
        _rankData.userID = GameManager.instance.firebaseAuthManager.userID;
        _rankData.userName = GameManager.instance.dataManager.playerData.shopName + "#" +
                             _rankData.userID.Substring(0, 2);

        RankData rankData = new RankData();
        rankData = _rankData;
        string jsonData = JsonUtility.ToJson(rankData);
        _reference.Child(FirebaseDataType.RankData.ToString()).Child(GameManager.instance.firebaseAuthManager.userID).SetRawJsonValueAsync(jsonData);
        Debug.Log("save");
    }

    public void FindData(string data)
    {
        
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

    // public string FindRankData(FirebaseDataType title, string method, string find, string findType)
    // {
    //     _reference.Child(title.ToString()).GetValueAsync().ContinueWithOnMainThread(task =>
    //     {
    //         if (task.IsCanceled || task.IsFaulted)
    //         {
    //             Debug.Log("랭킹 탑10 불러오기 실패");
    //             return null;
    //         }
    //         
    //         DataSnapshot snapshot = task.Result;
    //
    //         foreach (var childSnapshot in snapshot.Children)
    //         {
    //             if (childSnapshot.Child(method) == null || childSnapshot.Child(findType) == null)
    //             {
    //                 Debug.Log("찾는 항목이 없습니다");
    //                 return null;
    //             }
    //             
    //             if( childSnapshot.Child(method).Value.ToString() != find)
    //                 continue;
    //             
    //             return childSnapshot.Child(findType).Value.ToString();
    //         }
    //
    //         return null;
    //     });
    //     
    //     return null;
    // }

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

                    foreach (var childSnapshot in snapshot.Children)
                    {
                        if (title == FirebaseDataType.RankData)
                        {
                            rankList.Enqueue((childSnapshot.Child("userName").Value.ToString(),
                                int.Parse(childSnapshot.Child("earnedPerDay").Value.ToString())));
                        }
                    }
                    Debug.Log(rankList.Count);
                    
                    OnRoadRankData?.Invoke(rankList);
                });
    }
    
    public int LoadPlayerRankData(FirebaseDataType title, string type)
    {
        _reference.Child(title.ToString()).OrderByChild(type).LimitToLast(100).GetValueAsync()
            .ContinueWithOnMainThread(
                task =>
                {
                    int count = 1;

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
                            Debug.Log("INININININasadasd");
                            if (childSnapshot.Child("userId").Value.ToString() != _rankData.userID)
                            {
                                count++;
                                Debug.Log("INININININ");
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
