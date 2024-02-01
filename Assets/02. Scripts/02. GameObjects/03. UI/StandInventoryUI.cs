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
    private AbstractInventory curInventory;
    private InstallationController controller;

    private void Awake()
    {
        controller = GetComponentInParent<InstallationController>();
    }

    private void OnEnable()
    {
        if (controller != null)
        {
            //controller.deliverInventoryInfo += HandleInventoryInfo;
        }
    }

    private void OnDisable()
    {
        if (controller != null)
        {
            //controller.deliverInventoryInfo -= HandleInventoryInfo;
        }
    }

    private void HandleInventoryInfo(AbstractInventory obj)
    {
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

        foreach (var item in curInventory.Items)
        {
            if (slotCount >= 3) break; // 슬롯 개수 제한

            if (item.Key.type == 2 || item.Key.type == 3)
            {
                SetItemInfo(item.Key, item.Value);
                slotCount++;
            }
        }
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
