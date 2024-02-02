using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class StandInventoryUI : UIBase
{
    public UIBase playerInventory;
    public GameObject inventoryPanel;
    public GameObject inventorySlotPrefab;
    public Transform inventoryContentsParent;

    private AbstractInventory inventory;
    public AbstractInventory curInventory;
    private InstallationController controller;

    private void Start()
    {
        controller = GetComponentInParent<InstallationController>();
    }

    private void OnEnable()
    {
        AbstractInventory.OnInventoryClicked += HandleInventoryInfo;
        Debug.Log($"HJandled with OnInventoryClickeD. ");
        GameManager.instance.inventoryManager.OnInventoryUpdated += HandleInventoryUpdate;
    }

    private void OnDisable()
    {

        AbstractInventory.OnInventoryClicked -= HandleInventoryInfo;
        GameManager.instance.inventoryManager.OnInventoryUpdated -= HandleInventoryUpdate;
    }

    private void HandleInventoryUpdate(int inventoryID)
    {
        if (this.inventory != null && this.inventory.inventoryID == inventoryID)
        {
            UpdateUI();
        }
    }
    private void HandleInventoryInfo(AbstractInventory obj)
    {
        if (obj == null)
        {
            Debug.LogError("Received null inventory object");
            return;
        }

        Debug.Log("HandleInventoryInfo called with inventory ID: " + obj.inventoryID);

        OpenStandInventory(obj);
        curInventory = obj;
        UpdateUI();
    }

    public override void Initialize()
    {
        
    }

    public override void UpdateUI()
    {
        if (curInventory != null)
            UpdateStandInventory(curInventory);
    }

    public void OpenStandInventory(AbstractInventory inventory)
    {
        this.inventory = inventory;
        UpdateStandInventory(inventory);
    }
    private void UpdateStandInventory(AbstractInventory curInventory)
    {
        ClearInventoryDisplay();
        CreateItemSlots(curInventory);
    }

    private void ClearInventoryDisplay()
    {
        foreach (Transform child in inventoryContentsParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void CreateItemSlots(AbstractInventory curInventory)
    {
        int slotCount = 0;
        List<ItemSO> itemToRemove = new List<ItemSO>();
        
        foreach (var item in curInventory.Items)
        {
            if (slotCount >= 3 || item.Key.type ==1)
            {
                itemToRemove.Add(item.Key);
                continue;
            }
            if (item.Key.type == 2 || item.Key.type == 3)
            {
                SetItemInfo(item.Key, item.Value);
                slotCount++;
            }
        }

        foreach (ItemSO item in itemToRemove)
        {
            ReturnToPlayerInventory(item, curInventory.Items[item]);
        }
    }

    private void ReturnToPlayerInventory(ItemSO key, int value)
    {
        int playerInventoryID = FindObjectOfType<ShopInventory>().inventoryID;
        GameManager.instance.inventoryManager.TransferItem(curInventory.inventoryID, playerInventoryID, key, value);
        curInventory.Items.Remove(key);
    }

    private void SetItemInfo(ItemSO key, int value)
    {
        GameObject slotUI = Instantiate(inventorySlotPrefab, inventoryContentsParent);
        SetupItemSlot(slotUI, key, value);
    }
    private void SetupItemSlot(GameObject itemSlotObject, ItemSO item, int quantity)
    {
        ItemSlotInfo itemSlotInfo = itemSlotObject.GetComponent<ItemSlotInfo>();
        if (itemSlotInfo != null)
        {
            itemSlotInfo.Setup(item, quantity);
        }
    }
    public void OpenPlayerInventory()
    {
        GameManager.instance.uiManager.OpenWindow(playerInventory, true);
    }
}
