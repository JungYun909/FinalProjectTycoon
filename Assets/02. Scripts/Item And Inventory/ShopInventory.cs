using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEntry
{
    public ItemSO item;
    public int quantity;
}    //인벤토리 인스펙터로 검토하기 위한 필드, 추후 삭제 예정

public class ShopInventory : AbstractInventory
{
    private Dictionary<ItemSO, int> items = new Dictionary<ItemSO, int>();

    [SerializeField]
    private List<ItemEntry> debugItemList; // 디버그 목적으로만 사용

    public override void AddItem(ItemSO item, int quantity)
    {
        if (!items.ContainsKey(item))    // 아이템이 딕셔너리에 있는지 판별
        {
            items[item] = 0;
        }
        items[item] += quantity;
        UpdateDebugItemList(); // 디버그 리스트 업데이트
        // TODO 인벤토리창 업데이트 로직
    }

    public override bool RemoveItem(ItemSO item, int quantity)
    {
        if (items.ContainsKey(item) && items[item] >= quantity)
        {
            items[item] -= quantity;
            if (items[item] == 0)
            {
                items.Remove(item);
            }
            UpdateDebugItemList(); // 디버그 리스트 업데이트
            // TODO 인벤토리창 업데이트 로직
            return true;
        }
        return false;
    }

    private void UpdateDebugItemList()   // 디버그용 인스펙터업데이트
    {
        debugItemList.Clear();
        foreach (var entry in items)
        {
            debugItemList.Add(new ItemEntry { item = entry.Key, quantity = entry.Value });
        }
    }

    public void AddTestItem1() { AddItemThroughManager(1); }
    public void AddTestItem2() { AddItemThroughManager(2); }
    public void AddTestItem3() { AddItemThroughManager(3); }
    public void AddTestItem4() { AddItemThroughManager(4); }

    private void AddItemThroughManager(int itemID)
    {
        ItemSO item = inventoryManager.itemDatabase.GetItemByID(itemID);
        if (item != null)
        {
            inventoryManager.AddItemToInventory(inventoryID, item, 1);
        }
    }
}