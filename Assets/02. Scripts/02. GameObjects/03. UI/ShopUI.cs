using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    public int shopInventoryID;
    public  ItemSO curItem;

    public Button machinePurchase;
    public Button itemPurchase;
    private int amountToPay;
    private int quantityToPurchase;

    public event Action onMachineEnabled;
    public event Action onIngredientEnabled;
    private void Start()
    {
        quantityCheck.SetActive(false);
        shopInventoryID = FindObjectOfType<ShopInventory>().inventoryID;
    }
    public override void Initialize()
    {
        
    }
    
    public override void UpdateUI()
    {

    }
    private void HandleItemInfo(ItemSO obj)
    {
        curItem = obj;
        UpdateItemInfoInItemInfoWindow();
    }

    private void ClearUI()
    {
        datas.Clear();
        foreach (Transform child in slotParent)
        {
            ItemSlotInfo slotInfo = child.GetComponent<ItemSlotInfo>();
            if (slotInfo != null)
            {
                slotInfo.DeliverItem -= HandleItemInfo;
            }
            Destroy(child.gameObject);
        }
        quantityCheck.SetActive(false);
        foreach (Transform slot in slotParent)
        {
            Destroy(slot.gameObject);
        }
        if (slotParent.childCount > 0)
            return;
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
        onMachineEnabled?.Invoke();
    }

    private void UpdateTutoMachineInfoToShopUI(MachineSO data)
    {
        ClearUI();
        GameObject slot = Instantiate(slotPrefab, slotParent);
        slot.GetComponent<Image>().sprite = data.sprite;
        ItemDataContainer curItemData = slot.GetComponent<ItemDataContainer>();
        datas.Add(curItemData);
        curItemData.machineSO = data;

        datas[0].seeItemData += SetInfo;
        onMachineEnabled?.Invoke();
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
        onIngredientEnabled?.Invoke();

    }
    public void OpenIngredientShopUI()
    {
        UpdateItemInfoToShopUI();
        foreach (Transform child in slotParent)
        {
            ItemSlotInfo slotInfo = child.GetComponent<ItemSlotInfo>();
            if (slotInfo != null)
            {
                slotInfo.DeliverItem += HandleItemInfo;
            }
        }
    }

    public void OpenMachinShopUI()
    {
        Debug.Log(GameManager.instance.dataManager.playerData.tutoNum);
        if (GameManager.instance.dataManager.playerData.tutoNum == 4)
        {
            UpdateTutoMachineInfoToShopUI(GameManager.instance.dataManager.installationSub[0]);
            return;
        }
        else if(GameManager.instance.dataManager.playerData.tutoNum == 12)
        {
            UpdateTutoMachineInfoToShopUI(GameManager.instance.dataManager.installationSub[2]);
            return;
        }
        else if(GameManager.instance.dataManager.playerData.tutoNum == 26)
        {
            UpdateTutoMachineInfoToShopUI(GameManager.instance.dataManager.installationSub[4]);
            return;
        }
        UpdateMachineInfoToShopUI();
    }
    private void SetInfo(MachineSO sO)
    {
        machineSO = sO;
        nameText.text = sO.installasionName;
        descriptionText.text = sO.description;
        priceText.text = sO.price.ToString() + "원";
    }

    //public void SpawnInstallation()
    //{
    //    if (machineSO.price > GameManager.instance.statManager.currentGold)
    //    {
    //        Debug.Log("돈이 부족해요"); //TODO 유아이 경고 창 띄우기
    //        return;
    //    }
    //    GameManager.instance.statManager.SpendGold(machineSO.price);

    //    GameObject obj = GameManager.instance.spawnManager.SpawnInstallaion(machineSO);
    //    GameManager.instance.uiManager.CloseAll();
    //}

    private void UpdateItemInfoInItemInfoWindow()
    {
        nameText.text = curItem.itemName;
        descriptionText.text = curItem.description;
        priceText.text = curItem.price.ToString();
    }

    public void CloseShop()
    {
        GameManager.instance.uiManager.CloseAll();
    }

}
