using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "ItemData000", menuName = "SO by BW/ItemDatabase", order = 0)]

public class ItemDatabaseSO : ScriptableObject
{
    public List<ItemSO> itemDataList = new List<ItemSO>();

    public ItemSO GetItemByID(int id)
    {
        foreach (var item in itemDataList)
        {
            if (item.id == id)
            {
                return item;
            }
        }
        return null;
    }
}
