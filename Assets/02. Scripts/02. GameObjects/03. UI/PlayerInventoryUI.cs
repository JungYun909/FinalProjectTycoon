using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventoryUI : UIBase
{
    public UIBase inventoryPanelPrefab; // 인벤토리 패널 프리팹 참조
    private GameObject inventoryPanelInstance; // 인벤토리 패널 인스턴스

    public GameObject inventoryItemPrefab; // 인벤토리 아이템 프리팹 참조
    public Transform inventoryItemsParent; // 인벤토리 아이템을 보여줄 부모 객체

    private ShopInventory playerInventory; // 플레이어 인벤토리 데이터
    

    private void Start()
    {
        playerInventory = FindObjectOfType<ShopInventory>();
    }

    // 인벤토리 UI를 활성화하는 메서드
    public void OpenInventory()
    {
        GameManager.instance.uiManager.OpenWindow(inventoryPanelPrefab, true, playerInventory);
        UpdateUI();
    }

    // 인벤토리 UI를 비활성화하는 메서드
    public void CloseInventory()
    {
        if (inventoryPanelInstance != null)
        {
            inventoryPanelInstance.SetActive(false);
        }
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
        SetupItemSlot(itemUI, item, quantity);
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
}
