using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Resource,
    Goods,
    Installation
}
public abstract class ItemData : MonoBehaviour
{
    [Header("Info")]
    public string displayName;
    public string description;
    public int price;
    public ItemType type;
    public Sprite icon;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;
}

