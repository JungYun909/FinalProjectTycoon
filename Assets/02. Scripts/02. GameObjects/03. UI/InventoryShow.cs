using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryShow : UIBase
{
    public GameObject inventoryPanel; // 인벤토리 패널 UI 참조
    public GameObject inventoryItemPrefab; // 인벤토리 아이템 프리팹 참조
    public Transform inventoryItemsParent; // 일반 인벤토리 아이템을 보여줄 부모 객체
    public Transform specialDoughItemsParent; // 반죽 아이템을 보여줄 특별한 부모 객체
    AbstractInventory inventory;

    private void OnEnable()
    {
        AbstractInventory.OnInventoryClicked += HandleInventoryOpened;
        InventoryManager.instance.OnInventoryUpdated += HandleInventoryUpdate;

    }

    private void OnDisable()
    {
        AbstractInventory.OnInventoryClicked -= HandleInventoryOpened;
        InventoryManager.instance.OnInventoryUpdated -= HandleInventoryUpdate;

    }

    private void HandleInventoryOpened(AbstractInventory inventory)
    {
        // 이벤트가 발생했을 때 UI를 업데이트합니다.
        OpenInventory(inventory);
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
        foreach (Transform child in specialDoughItemsParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void CreateItemSlots(AbstractInventory inventory)
    {
        // 인벤토리의 각 아이템에 대한 UI 생성
        foreach (var item in inventory.Items)
        {
            if (item.Key.id == 1) // 반죽 아이템(id가 1) 처리
            {
                CreateDoughSlots(item.Key, item.Value);
            }
            else // 다른 아이템 처리
            {
                CreateNormalItemSlot(item.Key, item.Value);
            }
        }
    }

    private void CreateDoughSlots(ItemSO doughItem, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject doughUI = Instantiate(inventoryItemPrefab, specialDoughItemsParent);
            SetupItemSlot(doughUI, doughItem, 1); // 각 반죽은 개별 슬롯에 들어감
        }
    }

    private void CreateNormalItemSlot(ItemSO item, int quantity)
    {
        GameObject itemUI = Instantiate(inventoryItemPrefab, inventoryItemsParent);
        SetupItemSlot(itemUI, item, quantity);
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
        {
            UpdateInventoryDisplay(inventory);
        }
    }
}