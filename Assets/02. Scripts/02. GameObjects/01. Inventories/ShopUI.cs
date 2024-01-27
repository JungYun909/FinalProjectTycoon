using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopUI : UIBase
{
    public List<MachineSO> machines;
    public GameObject slotPrefab;
    public Transform slotParent;
    public TMP_Text nameText;
    public List<ItemDataContainer> datas;
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
            ItemDataContainer curItemData = slot.GetComponent<ItemDataContainer>();
            datas.Add(curItemData);
            curItemData.machineSO = machine;
        }

        foreach (var data in datas)
        {
            data.seeItemData += SetInfo;
        }
    }

    private void SetInfo(MachineSO sO)
    {
        nameText.text = sO.installasionName;
    }
}