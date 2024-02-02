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
    public GameObject quantityCheck;
    private ItemDatabaseSO itemData;
    private int shopInventoryID;

    private void Start()
    {
        quantityCheck.SetActive(false);
    }
    public override void Initialize()
    {
        
    }
    
    public override void UpdateUI()
    {

    }
    private void ClearUI()
    {
        quantityCheck.SetActive(false);
        foreach (Transform slot in slotParent)
        {
            Destroy(slot.gameObject);
            Debug.Log("인벤토리 부숴짐");
        }
<<<<<<< Updated upstream
=======

        if (slotParent.childCount > 0)
            return;
>>>>>>> Stashed changes
    }

    private void UpdateMachineInfoToShopUI()
    {
        ClearUI();
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
        ClearUI();
        itemData = GameManager.instance.inventoryManager.itemDatabase;
        foreach (ItemSO item in itemData.itemDataList)
        {
            if (item.type == 1 && item.id != 1)
            {
                GameObject slot = Instantiate(itemSlot, slotParent);
                ItemSlotInfo itemSlotInfo = slot.GetComponent<ItemSlotInfo>();
                if (itemSlotInfo != null)
                {
                    itemSlotInfo.Setup(item, 1);
                }
            }
            else
            {
                continue;
            }
        }
    }
    public void OpenIngredientShopUI()
    {
        UpdateItemInfoToShopUI();
        quantityCheck.SetActive(true);
    }

    public void OpenMachinShopUI()
    {
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


    //public void PurchaseItem()
    //{
    //    shopInventoryID = FindObjectOfType<ShopInventory>().inventoryID;
    //    int quantity = int.Parse(quantityInput.text);
    //    ItemSO item
    //    GameManager.instance.inventoryManager.AddItemToInventory(shopInventoryID, item, quantity);
    //    GameManager.instance.statManager.SpendGold(item.price * quantity);
    //}
}
