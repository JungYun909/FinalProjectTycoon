using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public ItemDatabaseSO itemDatabase;  // 아이템 데이터베이스 참조
    private Dictionary<int, AbstractInventory> inventories = new Dictionary<int, AbstractInventory>();   // 인벤토리를 딕셔너리로 정리
    private int nextInventoryID = 1000;   // 인덱스용 아이디를 부여하기 위한 필드

    private UIManager  inventoryUIUpdator;

    private void Awake()
    {
        inventoryUIUpdator = FindObjectOfType<UIManager>();
    }

    public int RegisterInventory(AbstractInventory inventory)    // 새로 생기는 인벤토리를 딕셔너리에 등재하기 위한 메서드. 모든 인벤토리 생성시
    {
        int inventoryID = nextInventoryID++;
        inventories[inventoryID] = inventory;
        return inventoryID;
    }

    public void AddItemToInventory(int inventoryID, ItemSO item, int quantity, GameObject gameObject)
    {
        if (inventories.ContainsKey(inventoryID))
        {
            var inventory = inventories[inventoryID];
            if (!inventory.Items.ContainsKey(item))
            {
                inventory.Items[item] = 0;
            }
            inventory.Items[item] += quantity;
            // 인벤토리 UI 업데이트를 위한 items 딕셔너리 전달
            inventoryUIUpdator.UpdateInventoryUI(inventory.Items, gameObject);
            inventory.UpdateInspectorList();
        }
    }

    public bool RemoveItemFromInventory(int inventoryID, ItemSO item, int quantity)
    {
        if (inventories.ContainsKey(inventoryID))
        {
            var inventory = inventories[inventoryID];
            if (inventory.Items.ContainsKey(item) && inventory.Items[item] >= quantity)
            {
                inventory.Items[item] -= quantity;
                if (inventory.Items[item] == 0)
                {
                    inventory.Items.Remove(item);
                }

                inventoryUIUpdator.UpdateInventoryUI(inventory.Items, gameObject);
                inventory.UpdateInspectorList();

                return true;
            }
        }
        return false;
    }

    public void TransferItem(int fromInventoryID, int toInventoryID, ItemSO item, int quantity)
    {
        if (inventories.ContainsKey(fromInventoryID) && inventories.ContainsKey(toInventoryID))
        {
            var fromInventory = inventories[fromInventoryID].Items;
            var toInventory = inventories[toInventoryID].Items;

            // 아이템을 이동시킬 수 있는지 확인
            if (fromInventory.ContainsKey(item) && fromInventory[item] >= quantity)
            {
                // 아이템을 기존 인벤토리에서 제거
                fromInventory[item] -= quantity;
                if (fromInventory[item] == 0)
                {
                    fromInventory.Remove(item);
                }

                // 아이템을 새 인벤토리에 추가
                if (!toInventory.ContainsKey(item))
                {
                    toInventory[item] = 0;
                }
                toInventory[item] += quantity;

                // 인벤토리 UI 업데이트
            }
        }
    }
    // 추가로 필요한 Method?
}