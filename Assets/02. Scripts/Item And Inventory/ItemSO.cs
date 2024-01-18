using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemData000", menuName = "SO by BW/Item", order = 1)]
public class ItemSO : ScriptableObject
{
    public int id;
    public string itemName;
    public int price;
}
