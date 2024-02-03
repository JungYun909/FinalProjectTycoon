using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerInventoryUI : UIBase
{
    public UIBase inventoryPanelPrefab; // 인벤토리 패널 프리팹 참조
    private GameObject inventoryPanelInstance; // 인벤토리 패널 인스턴스

    public GameObject inventoryItemPrefab; // 인벤토리 아이템 프리팹 참조
    public Transform inventoryItemsParent; // 인벤토리 아이템을 보여줄 부모 객체

    private ShopInventory playerInventory; // 플레이어 인벤토리 데이터
    AbstractInventory inventory;
    private ItemSlotInfo itemSlot;

    public GameObject quantityController;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI priceText;

    public ItemSO curItem;
    public int curInventoryID;


    private void Start()
    {
        playerInventory = FindObjectOfType<ShopInventory>();
        itemSlot = GetComponentInChildren<ItemSlotInfo>();
        quantityController.SetActive(false);
    }
    private void OnEnable()
    {
        GameManager.instance.inventoryManager.OnInventoryUpdated += HandleInventoryUpdate;
        if (itemSlot != null)
        {
        }
    }

    private void OpenQuantityController()
    {
        quantityController.SetActive(true);
    }

    private void OnDisable()
    {
        GameManager.instance.inventoryManager.OnInventoryUpdated -= HandleInventoryUpdate;
        if (itemSlot != null)
        {
        }
    }

    private void HandleInventoryUpdate(int inventoryID)
    {
        if (playerInventory != null && playerInventory.inventoryID == inventoryID)
        {
            UpdateUI();
        }
    }

    // 인벤토리 UI를 활성화하는 메서드
    public void OpenInventory()
    {
        GameManager.instance.uiManager.OpenWindow(inventoryPanelPrefab, true, playerInventory);
        UpdateUI();
    }

    // 기존 아이템 UI를 제거하는 메서드
    private void ClearInventoryDisplay()
    {
        foreach (Transform child in inventoryItemsParent)
        {
            Destroy(child.gameObject);
        }
    }

    // 아이템 슬롯 생성 메서드
    private void CreateItemSlot(ItemSO item, int quantity)
    {
        GameObject itemUI = Instantiate(inventoryItemPrefab, inventoryItemsParent);
        ItemSlotInfo itemSlotInfo = itemUI.GetComponent<ItemSlotInfo>();
        if (itemSlotInfo != null)
        {
            itemSlotInfo.Setup(item, quantity);
            itemSlotInfo.DeliverItem += UpdateItemData; // 이벤트 구독 추가
            itemSlotInfo.DeliverInventoryInfo += OpenQuantityController;

        }

    }
    private void UpdateItemData(ItemSO item)
    {
        curItem = item;
        UpdateItemInfoInItemInfoWindow();
    }
    // 아이템 슬롯 설정 메서드
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
        
    }

    public override void UpdateUI()
    {
        playerInventory = FindObjectOfType<ShopInventory>();
        ClearInventoryDisplay(); // 기존 UI 요소 제거
        foreach (var itemEntry in playerInventory.Items)
        {
            var item = itemEntry.Key;
            var quantity = Mathf.Min(itemEntry.Value, 99); // 최대 수량 99로 제한

            CreateItemSlot(item, quantity);
        }
    }

    public void CloseWindow()
    {
        GameManager.instance.uiManager.CloseAll();
    }

    private void UpdateItemInfoInItemInfoWindow()
    {
        nameText.text = curItem.itemName;
        descriptionText.text = curItem.description;
        priceText.text = curItem.price.ToString();
    }
}