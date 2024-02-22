using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
