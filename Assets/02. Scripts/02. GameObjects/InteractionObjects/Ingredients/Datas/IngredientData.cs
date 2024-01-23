using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct IngredientStat
{
    [Header("Info")] 
    public string name;
    public string discription;

    [Header("Movement")]
    public float moveSpeed;

    [Header("VisitInformation")]
    public List<GameObject> VisitGameObjects;
}
public abstract class IngredientData : MonoBehaviour, IInteractable
{
    public IngredientStat stat;
    
    public abstract void InitSetting();
    public abstract bool Continuous();

    public void OnClickInteract()
    {
        //음식을 클릭했을때 뭔가가 일어난다면
        return;
    }

    public void OnColliderInteract()
    {
        PoolManager.instacne.DeSpawnFromPool(gameObject);
    }
}
