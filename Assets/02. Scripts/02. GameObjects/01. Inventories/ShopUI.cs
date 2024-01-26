using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : UIBase
{
    public List<MachineSO> machines;
    public GameObject slotPrefab;
    public Transform slotParent;
    public TMP_Text data;
    public override void Initialize()
    {
        Debug.Log("dd");
    }

    public override void UpdateUI()
    {
        foreach (Transform slot in slotParent)
        {
            Destroy(slot);
        }

        foreach (MachineSO machine in machines)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            slot.GetComponent<Image>().sprite = machine.sprite;
            ItemDataContainer itemDataContainer = slot.GetComponent<ItemDataContainer>();
            itemDataContainer.machineSO = machine;
            itemDataContainer.seeItemData += SetInfo;
        }
    }

    private void SetInfo(MachineSO sO)
    {
        data.text = sO.name;
    }
}
