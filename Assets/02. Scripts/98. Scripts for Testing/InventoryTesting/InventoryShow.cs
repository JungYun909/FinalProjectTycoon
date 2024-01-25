using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryShow : MonoBehaviour
{ 
    public GameObject inventoryPanel; // 인벤토리 패널 UI 참조
    public GameObject inventoryItemPrefab; // 인벤토리 아이템 프리팹 참조
    public Transform inventoryItemsParent; // 인벤토리 아이템을 보여줄 부모 객체

    private void Start()
    {
        inventoryPanel.SetActive(false);
    }

    public void OpenInventory(AbstractInventory inventory)
    {
        inventoryPanel.SetActive(true);
        UpdateInventoryDisplay(inventory);
    }

    private void UpdateInventoryDisplay(AbstractInventory inventory)
    {
        // 기존 아이템 UI를 모두 제거
        foreach (Transform child in inventoryItemsParent)
        {
            Destroy(child.gameObject);
        }

        // 인벤토리의 각 아이템에 대한 UI 생성
        foreach (var item in inventory.Items)
        {
            GameObject itemUI = Instantiate(inventoryItemPrefab, inventoryItemsParent);
            ItemSlotInfo itemSlotInfo = itemUI.GetComponent<ItemSlotInfo>();
            if (itemSlotInfo != null)
            {
                // ItemSlotInfo를 사용하여 각 슬롯을 설정
                itemSlotInfo.Setup(item.Key, item.Value);
            }
        }
    }
}