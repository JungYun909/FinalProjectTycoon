using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public enum InteractionType
{
    Ingredient,
    Goods,
    Installation,
    UI
}

public struct InteractionStat
{
    [Header("Info")]
    public string name;
    public string description;
    public InteractionType type;
    public Sprite icon;

    [Header("Celling")]
    public bool canCell;
    public int price;

    [Header("Moving")] 
    public bool canMove;
    public float speed;
    public GameObject destinationGameObject;

    [Header("UI")]
    public GameObject curGameObject;
}

public abstract class InteractionData: MonoBehaviour, IInteractable
{
    public InteractionStat _interactionStat;
    
    public abstract void InitSetting();
    public abstract bool Continuous();

    public abstract void OnInteract();
}


