using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    Btn,
}

public struct UIStat
{
    [Header("Info")]
    public string name;
    public UIType type;
    public GameObject Prefab;
}
public abstract class UIData : MonoBehaviour, IInteractable
{
    protected UIStat _uiStat;
    protected Vector2 mouseDirection;

    private void Awake()
    {
        InputManager.instance.OnLookEvent += MouseDirectionUpdate;
        InputManager.instance.OnClickEvent += OnInteract;
    }

    private void MouseDirectionUpdate(Vector2 curMouseDirection)
    {
        mouseDirection = curMouseDirection;
    }

    public abstract void InitSetting();
    public abstract void OnInteract();
}
