using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaSlotController : MonoBehaviour
{
    public ItemSO data;
    public Image image;

    private void Start()
    {
        InitSetting();
    }

    public void InitSetting()
    {
        image.color = Color.gray;
        image.sprite = data.sprite;
    }
}
