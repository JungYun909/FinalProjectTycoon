using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketStanding : MonoBehaviour
{
    public int food; //음식 ID(샘플)
    public int foodnum; // 음식 갯수(샘플)
    // Start is called before the first frame update
    void Awake()
    {
        food = 1;
        foodnum = 10;
    }

}
