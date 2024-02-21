using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDatabaseSO : ScriptableObject
{
    public List<NpcSO> npcDataList = new List<NpcSO>();

    public NpcSO GetItemByID(int id)
    {
        foreach (var item in npcDataList)
        {
            if (item.id == id)
            {
                return item;
            }
        }
        return null;
    }
}
