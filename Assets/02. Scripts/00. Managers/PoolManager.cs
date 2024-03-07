using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : MonoBehaviour            // spawn/despawn 생성, 해제 등과 관련된 업무 전체 관	
{
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
    }

    private GameObject AddTagToDictionary(GameObject addGameObject)
    {
        if (!poolDictionary.ContainsKey(addGameObject.tag))
        {
            poolDictionary.Add(addGameObject.tag, new Queue<GameObject>());
        }
        GameObject obj = Instantiate(addGameObject);
        return obj;
    }

    public void DeSpawnFromPool(GameObject addGameObject)
    {
        addGameObject.SetActive(false);
        poolDictionary[addGameObject.tag].Enqueue(addGameObject);
    }
    public GameObject SpawnFromPool(GameObject addGameObject)
    {
        if (IsMakeNew(addGameObject))
        {
            return AddTagToDictionary(addGameObject);
        }
        GameObject obj = poolDictionary[addGameObject.tag].Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public bool IsMakeNew(GameObject addGameObject)
    {
        if (!poolDictionary.ContainsKey(addGameObject.tag) || poolDictionary[addGameObject.tag].All(o => o.activeSelf))
        {
            return true;
        }

        return false;
    }

    public void ResetPool()
    {
        poolDictionary.Clear(); 
    }
}
