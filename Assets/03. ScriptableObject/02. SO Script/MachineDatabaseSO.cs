using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineDatabaseSO : ScriptableObject
{
    public List<MachineSO> machineDataList = new List<MachineSO>();

    public MachineSO GetItemByID(int id)
    {
        foreach (var item in machineDataList)
        {
            if (item.id == id)
            {
                return item;
            }
        }
        return null;
    }
}
