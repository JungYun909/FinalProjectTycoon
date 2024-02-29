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

    public Button installConfirm;
    public GameObject quantityController;
    public int quantity;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI priceText;

    public ItemSO curItem;
    public int itemQuantityInInventory;

    public MachineSO curMachine;
    public int machineQuantityInInventory;

    public int curInventoryID;
    private int whatToShow = 1;

    private void Start()
    {
        playerInventory = FindObjectOfType<ShopInventory>();
        itemSlot = GetComponentInChildren<ItemSlotInfo>();
        quantityController.SetActive(false);
    }
    private void OnEnable()
    {
        GameManager.instance.inventoryManager.OnInventoryUpdated += HandleInventoryUpdate;
    }

    private void OpenQuantityController()
    {
        quantityController.SetActive(true);
        QuantityController controller = quantityController.GetComponent<QuantityController>();

        if (controller != null)
        {
            controller.DeliverQuantity -= HandleTransfer;
            controller.DeliverQuantity += HandleTransfer;
            // PlayerInventoryUI에서 아이템의 수량을 기반으로 최대 수량 설정
            controller.SetMaxQuantity(itemQuantityInInventory);
        }
    }

    private void HandleTransfer(int quantity)
    {
        if (quantity == 0)
            return;
        if (quantity <= itemQuantityInInventory)
        {
            GameManager.instance.inventoryManager.TransferItem(1000, curInventoryID, curItem, quantity);
            if (quantityController != null)
            {
                QuantityController controller = quantityController.GetComponent<QuantityController>();
                if (controller != null)
                {
                    controller.DeliverQuantity -= HandleTransfer;
                }
            }
            GameManager.instance.uiManager.GoBack();
        }
        else
            return;
    }

    private void OnDisable()
    {
        GameManager.instance.inventoryManager.OnInventoryUpdated -= HandleInventoryUpdate;
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
            itemSlotInfo.DeliverInventoryID += SetCurInventoryID;
        }
    }

    private void CreateMachineSlot(MachineSO machine, int quantity)
    {
        GameObject machineUI = Instantiate(inventoryItemPrefab, inventoryItemsParent);
        ItemSlotInfo itemSlotInfo = machineUI.GetComponent<ItemSlotInfo>();
        if(itemSlotInfo!= null)
        {
            itemSlotInfo.SetupMachineInfo(machine, quantity);
            itemSlotInfo.DeliverMachine += UpdateMachineData;
        }
    }
    private void SetCurInventoryID(int obj)
    {
        curInventoryID = obj;
    }

    private void UpdateItemData(ItemSO item)
    {
        curItem = item;
        if (playerInventory.Items.TryGetValue(curItem, out int quantity))
        {
            itemQuantityInInventory = quantity;
        }
        UpdateItemInfoInItemInfoWindow();
    }

    private void UpdateMachineData(MachineSO machine)
    {
        curMachine = machine;
        if(playerInventory.machines.TryGetValue(curMachine, out int quantity))
        {
            machineQuantityInInventory = quantity;
        }
        UpdateMachineInfoWindow();
    }
    // 아이템 슬롯 설정 메서드
    //private void SetupItemSlot(GameObject itemSlotObject, ItemSO item, int quantity)
    //{
    //    ItemSlotInfo itemSlotInfo = itemSlotObject.GetComponent<ItemSlotInfo>();
    //    if (itemSlotInfo != null)
    //    {
    //        itemSlotInfo.Setup(item, quantity);
    //    }
    //}

    public override void Initialize()
    {

    }

    public override void UpdateUI()
    {
        playerInventory = FindObjectOfType<ShopInventory>();
        ClearInventoryDisplay(); // 기존 UI 요소 제거
        ClearInfoWindow();
        switch (whatToShow)
        {
            case 1:
                UpdateProductInventory();
                break;

            case 2:
                UpdateItemInventory();
                break;
            case 3:
                UpdateMachineInventory();
                break;
            case 4:
                UpdateToolInventory();
                break;
        }
    }

    private void UpdateItemInventory()
    {
        installConfirm.gameObject.SetActive(false);
        foreach (var itemEntry in playerInventory.Items)
        {
            var item = itemEntry.Key;
            if (item.type == 1)
            {
                var quantity = Mathf.Min(itemEntry.Value, 99); // 최대 표시수량 99로 제한
                CreateItemSlot(item, quantity);
            }
        }
    }
    private void UpdateProductInventory()
    {
        installConfirm.gameObject.SetActive(false);
        foreach (var itemEntry in playerInventory.Items)
        {
            var item = itemEntry.Key;
            if (item.type != 1 && item.type != 4)
            {
                var quantity = Mathf.Min(itemEntry.Value, 99); // 최대 표시수량 99로 제한
                CreateItemSlot(item, quantity);
            }
        }
    }
    private void UpdateMachineInventory()
    {
        foreach (var itemEntry in playerInventory.machines)
        {
            var item = itemEntry.Key;
            var quantity = Mathf.Min(itemEntry.Value, 99);

            CreateMachineSlot(item, quantity);
        }
    }
    private void UpdateToolInventory()
    {
        installConfirm.gameObject.SetActive(false);
        foreach (var itemEntry in playerInventory.Items)
        {
            var item = itemEntry.Key;
            if (item.type == 4)
            {
                var quantity = Mathf.Min(itemEntry.Value, 99); // 최대 표시수량 99로 제한
                CreateItemSlot(item, quantity);
            }
        }
    }

    public void SetInventoryInfo(int number)
    {
        whatToShow = number;
        UpdateUI();
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

    private void UpdateMachineInfoWindow()
    {
        installConfirm.gameObject.SetActive(true);
        installConfirm.onClick.AddListener(SpawnInstallation);
        nameText.text = curMachine.installasionName;
        descriptionText.text = curMachine.description;
        priceText.text = curMachine.price.ToString();
    }

    private void ClearInfoWindow()
    {
        nameText.text = "";
        descriptionText.text = "";
        priceText.text = "";
    }

    private void SpawnInstallation()
    {
        if (curMachine != null)
        {
            GameObject obj = GameManager.instance.spawnManager.SpawnInstallaion(curMachine);
            GameManager.instance.inventoryManager.RemoveMachineFromPlayerInventory(curMachine, 1);
            GameManager.instance.uiManager.CloseAll();
        }
    }
}