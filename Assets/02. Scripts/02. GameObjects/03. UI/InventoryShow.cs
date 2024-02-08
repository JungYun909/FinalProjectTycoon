using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InventoryShow : UIBase
{
    public UIBase playerInventory;
    public GameObject inventoryPanel; // 인벤토리 패널 UI 참조
    public GameObject inventoryItemPrefab; // 인벤토리 아이템 프리팹 참조
    public Transform inventoryItemsParent; // 
    public Transform doughItemsParent; // 반죽 아이템을 보여줄 부모 객체
    public GameObject doughItemSlot;
    AbstractInventory inventory;


    public AbstractInventory curInventory;

    public event Action<int> DeliverInventoryID;

    private void OnEnable()
    {
        AbstractInventory.OnInventoryClicked += HandleInventoryOpened;
        GameManager.instance.inventoryManager.OnInventoryUpdated += HandleInventoryUpdate;

    }

    private void OnDisable()
    {
        AbstractInventory.OnInventoryClicked -= HandleInventoryOpened;
        GameManager.instance.inventoryManager.OnInventoryUpdated -= HandleInventoryUpdate;

    }

    private void HandleInventoryOpened(AbstractInventory inventory)
    {
        Debug.Log("Handled");
        // 이벤트가 발생했을 때 UI를 업데이트합니다.
        OpenInventory(inventory);
        curInventory = inventory;
        UpdateUI();
    }
    private void HandleInventoryUpdate(int inventoryID)
    {
        // 이벤트가 발생했을 때 UI를 업데이트합니다.
        if (this.inventory != null && this.inventory.inventoryID == inventoryID)
        {
            UpdateUI();
        }
    }

    public void OpenInventory(AbstractInventory inventory)
    {
        this.inventory = inventory;
        UpdateInventoryDisplay(inventory);
    }

    private void UpdateInventoryDisplay(AbstractInventory inventory)
    {
        ClearInventoryDisplay();
        CreateItemSlots(inventory);
    }

    private void ClearInventoryDisplay()
    {
        // 기존 일반 및 특별 아이템 UI를 모두 제거
        foreach (Transform child in inventoryItemsParent)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in doughItemsParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void CreateItemSlots(AbstractInventory inventory)
    {
        List<ItemSO> itemsToMove = new List<ItemSO>();
        // 인벤토리의 각 아이템에 대한 UI 생성
        foreach(var item in inventory.itemQueue)
        {
            CreateDoughSlots(item, 1);
        }

        foreach (var item in inventory.Items)
        {
            if (item.Key.type == 1 && item.Key.id != 1)
            {
                ItemSO previousItem = FindPreviousItemOfTypeOne(item.Key, inventory);

                if (previousItem != null)
                {
                    itemsToMove.Add(previousItem);
                }
            }

            if (item.Key.type == 3)
            {
                itemsToMove.Add(item.Key);
                continue;
            }

            if (item.Key.id == 1 || item.Key.type == 2) // 반죽 아이템(id가 1) 처리
            {
                continue;
            }

            else // 다른 아이템 처리
            {
                CreateNormalItemSlot(item.Key, item.Value);
            }
        }
        foreach (var item in itemsToMove)
        {
            ReturnToPlayerInventory(item);
        }
    }
    private ItemSO FindPreviousItemOfTypeOne(ItemSO newItem, AbstractInventory inventory)
    {
        foreach (var item in inventory.Items)
        {
            if (item.Key.type == 1 && item.Key.id != 1 && item.Key != newItem)
            {
                return item.Key;
            }
            Debug.Log($"PreivousItem:{ item.Key.itemName}");
        }
        return null;
    }
    private void CreateDoughSlots(ItemSO doughItem, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject doughUI = Instantiate(doughItemSlot, doughItemsParent);
            SetupItemSlot(doughUI, doughItem, 1); // 각 반죽은 개별 슬롯에 들어감
        }
    }

    private void CreateNormalItemSlot(ItemSO item, int quantity)
    {
        GameObject itemUI = Instantiate(inventoryItemPrefab, inventoryItemsParent);
        SetupItemSlot(itemUI, item, quantity);
    }

    private void ReturnToPlayerInventory(ItemSO item)
    {
        Debug.Log("Return!");
        int playerInventoryID = 1000;
        int curInventoryID = this.inventory.inventoryID;
        Debug.Log($"from {curInventoryID} to {playerInventoryID}");
        if (inventory.Items.TryGetValue(item, out int quantity))
        {
            GameManager.instance.inventoryManager.TransferItem(curInventoryID, playerInventoryID, item, quantity);
            Debug.Log($"Transfered {item.itemName}, {quantity}");
            //inventory.Items.Remove(item);
        }
    }

    private void SetupItemSlot(GameObject itemSlotObject, ItemSO item, int quantity)
    {
        ItemSlotInfo itemSlotInfo = itemSlotObject.GetComponent<ItemSlotInfo>();
        if (itemSlotInfo != null)
        {
            itemSlotInfo.Setup(item, quantity);
        }
    }

    public override void Initialize()
    {
        Debug.Log("DD");
    }
    
    public override void UpdateUI()
    {
        if (inventory != null)
            UpdateInventoryDisplay(inventory);
    }

    public void OpenPlayerInventory()
    {
        GameManager.instance.uiManager.OpenWindow(playerInventory, true);
        //DeliverInventoryID?.Invoke(curInventory.inventoryID);
    }
}