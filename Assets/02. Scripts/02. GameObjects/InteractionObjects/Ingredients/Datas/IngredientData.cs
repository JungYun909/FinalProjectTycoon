using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct IngredientStat
{
    [Header("Info")] 
    public string name;
    public string discription;
    public Sprite icon;

    [Header("Movement")]
    public float moveSpeed;

    [Header("VisitInformation")]
    public List<GameObject> VisitGameObjects;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;
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
        if(gameObject.tag == "Dough")
        {
            gameObject.SetActive(false);
        }
        else if(gameObject.tag == "Resource")
        {
            PoolManager.instacne.DeSpawnFromPool(gameObject);
        }
    }
}
