using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData item;

    private void Start()
    {
        item.InitSetting();
    }

    public void OnInteract()
    {
        item.OnInteract();
    }
}