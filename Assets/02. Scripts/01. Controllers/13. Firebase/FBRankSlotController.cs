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

    private void Start()
    {
        Color convertedColor = new Color(0.2941f, 0.1922f, 0.1765f, 1f);
        
        if (playerRank.text != "1")
        {
            playerRank.color = convertedColor;
            playerID.color = convertedColor;
            playerMoney.color = convertedColor;
        }
    }
}
