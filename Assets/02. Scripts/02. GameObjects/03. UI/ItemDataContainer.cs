using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataContainer : MonoBehaviour
{
    public MachineSO machineSO;
    public event Action<MachineSO> seeItemData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnItemData()
    {
        seeItemData?.Invoke(machineSO);
    }
}
