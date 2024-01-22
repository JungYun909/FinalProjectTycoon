using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InteractionObject : MonoBehaviour
{
    public InteractionData _interactionData;

    private void Start()
    {
        _interactionData.InitSetting();
    }
}