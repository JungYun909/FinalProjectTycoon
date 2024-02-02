using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ItemSlotInfo : MonoBehaviour      // 여기서 itemIcon, quantityText만 설정하고 업데이트는 UI에? 
{
    public Image itemIcon; // 아이템 아이콘을 표시할 Image 컴포넌트
    public TextMeshProUGUI quantityText; // 수량을 표시할 TextMeshProUGUI 컴포넌트

    public event Action<ItemSO> DeliverItem;
    private ItemSO curItem;

    private InventoryShow inventoryShow;
    private ShopInventory shopInventory;

    private void Awake()
    {
        inventoryShow = FindObjectOfType<InventoryShow>();
        shopInventory = FindObjectOfType<ShopInventory>();
    }
    public void Setup(ItemSO item, int quantity)
    {

        // 아이템 아이콘 설정
        if (item != null && item.sprite != null)
        {
            itemIcon.sprite = item.sprite;
            itemIcon.enabled = true; // 아이콘이 있다면 활성화
        }
        else
        {
            itemIcon.enabled = false; // 아이콘이 없다면 비활성화
        }
        curItem = item;
        // 수량 텍스트 설정
        quantityText.text = quantity > 0 ? quantity.ToString() : "";
    }

    private void OnEnable()
    {
        if (inventoryShow != null)
            inventoryShow.DeliverInventoryID += OnButtonClicked;
        else
            return;
    }

    private void OnDisable()
    {
        if (inventoryShow != null)
            inventoryShow.DeliverInventoryID -= OnButtonClicked;
        else
            return;
    }

    public void OnButtonClicked(int toInventoryID)
    {
        var inventoryShowInstance = FindObjectOfType<InventoryShow>();
        if (inventoryShowInstance != null && inventoryShowInstance.curInventory != null)
        {
            toInventoryID = inventoryShowInstance.curInventory.inventoryID;
        }
        else
        {
            var standInventoryUIInstance = FindObjectOfType<StandInventoryUI>();
            if (standInventoryUIInstance != null && standInventoryUIInstance.curInventory != null)
            {
                toInventoryID = standInventoryUIInstance.curInventory.inventoryID;
            }
            else
            {
                Debug.Log("No valid Inventory found");
                return;
            }
        }
        DeliverItemInfo(toInventoryID);
    }

    public void DeliverItemInfo(int toInventoryID)
    {
        if (curItem == null)
        {
            Debug.LogError("curItem is null");
            return;
        }

        if (shopInventory == null)
        {
            Debug.LogError("shopInventory is null");
            return;
        }

        if (toInventoryID != 0)
        {
            DeliverItem?.Invoke(curItem);
            Debug.Log(curItem.itemName);
            GameManager.instance.inventoryManager.TransferItem(shopInventory.inventoryID, toInventoryID, curItem, 1);
        }
        else
        {
            Debug.Log("No destination");
        }
    }
}