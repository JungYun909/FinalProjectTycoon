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
    public GameObject itemSlot;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text priceText;
    public List<ItemDataContainer> datas;
    public MachineSO machineSO;

    private ItemDatabaseSO itemData;
    public override void Initialize()
    {
        UpdateItemInfoToShopUI();
    }

    public override void UpdateUI()
    {
        foreach (Transform slot in slotParent)
        {
            Destroy(slot.gameObject);
            Debug.Log("인벤토리 부숴짐");
        }
        
        if(slotParent.childCount > 0)
            return;
    }

    private void UpdateMachineInfoToShopUI()
    {
        UpdateUI();
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

    private void UpdateItemInfoToShopUI()
    {
        UpdateUI();
        itemData = GameManager.instance.inventoryManager.itemDatabase;
        foreach (ItemSO item in itemData.itemDataList)
        {
            GameObject slot = Instantiate(itemSlot, slotParent);
            ItemSlotInfo itemSlotInfo = slot.GetComponent<ItemSlotInfo>();
            if (itemSlotInfo != null)
            {
                itemSlotInfo.Setup(item, 1);
            }
        }
    }
    public void OpenIngredientShopUI()
    {
        UpdateUI();
        UpdateItemInfoToShopUI();
    }

    public void OpenMachinShopUI()
    {
        UpdateUI();
        UpdateMachineInfoToShopUI();
    }
    private void SetInfo(MachineSO sO)
    {
        machineSO = sO;
        nameText.text = sO.installasionName;
        descriptionText.text = sO.description;
        priceText.text = sO.price.ToString() + "원";
    }

    public void SpawnInstallation()
    {
        if (machineSO.price > GameManager.instance.statManager.currentGold)
        {
            Debug.Log("돈이 부족해요"); //TODO 유아이 경고 창 띄우기
            return;
        }
        GameManager.instance.statManager.SpendGold(machineSO.price);
        
        GameObject obj = GameManager.instance.spawnManager.SpawnInstallaion(machineSO);
        GameManager.instance.uiManager.CloseAll();
    }
}
