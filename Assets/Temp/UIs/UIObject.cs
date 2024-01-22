using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObject : MonoBehaviour, IInteractable
{
    private UIData _uIData;

    private void Start()
    {
        _uIData.InitSetting();
    }

    public void OnInteract()
    {
        _uIData.OnInteract();
    }
}
