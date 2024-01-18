using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public enum ItemType
{
    Ingredient,
    Goods,
    Installation
}

public struct ItemStat
{
    [Header("Info")]
    public string name;
    public string description;
    public ItemType type;
    public Sprite icon;

    [Header("Celling")]
    public bool canCell;
    public int price;

    [Header("Moving")] 
    public bool canMove;
    public float speed;
    public GameObject destinationGameObject;
}

public abstract class ItemData: MonoBehaviour, IInteractable
{
    public ItemStat itemStat;
    
    public abstract void InitSetting();
    public virtual void OnInteract()
    {
        throw new System.NotImplementedException();
    }
}


