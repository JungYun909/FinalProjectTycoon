using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataContainer : MonoBehaviour
{
    public MachineSO machineSO;
    public event Action<MachineSO> seeItemData;

    public void OnItemData()
    {
        seeItemData?.Invoke(machineSO);
    }

    
}
