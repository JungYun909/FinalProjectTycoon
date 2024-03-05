using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class FBRankSlotController : MonoBehaviour
{
    public TextMeshProUGUI playerRank;
    public TextMeshProUGUI playerID;
    public TextMeshProUGUI playerMoney;
    public Color convertedColor = Color.clear;

    private void Start()
    {
        if(convertedColor == Color.clear)
            return;
        
        playerRank.color = convertedColor;
        playerID.color = convertedColor;
        playerMoney.color = convertedColor;
    }
}
