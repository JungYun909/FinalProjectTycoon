using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataContainer : MonoBehaviour
{
    public MachineSO machineSO;
    public event Action<MachineSO> seeItemData;
    // Start is called before the first frame update

    public void OnItemData()
    {
        seeItemData?.Invoke(machineSO);
        GameObject obj = GameManager.instance.spawnManager.SpawnInstallaion(machineSO);
        GameManager.instance.uiManager.CloseAll();
    }
}
