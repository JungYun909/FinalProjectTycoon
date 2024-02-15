using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TemporaryStat : MonoBehaviour
{
    [SerializeField] public int fame;
    [SerializeField] public int gold;
    [SerializeField] public string shopName;
    [SerializeField] public int interiorScore;
    [SerializeField] public int financialScore;
    [SerializeField] public int goldUsed;
    [SerializeField] public float playerTime;
    [SerializeField] public int shopLevel;
    [SerializeField] public int dayTime;
    [SerializeField] public int debt;
    [SerializeField] public int warningCount;
}
