using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingItemDatabaseSO : ScriptableObject
{

    public List<ItemSO> sellingItemDataList = new List<ItemSO>();

    public ItemSO GetItemByID(int id)
    {
        foreach (var item in sellingItemDataList)
        {
            if (item.id == id && item.type == 2)
            {
                return item;
            }
        }
        return null;
    }
}
