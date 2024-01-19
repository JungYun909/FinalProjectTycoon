using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public ItemDatabaseSO itemDatabase;  // 아이템 데이터베이스 참조
    private Dictionary<int, AbstractInventory> inventories = new Dictionary<int, AbstractInventory>();   // 인벤토리를 딕셔너리로 정리
    private int nextInventoryID = 1000;   // 인덱스용 아이디를 부여하기 위한 필드

    public int RegisterInventory(AbstractInventory inventory)    // 새로 생기는 인벤토리를 딕셔너리에 등재하기 위한 메서드. 모든 인벤토리 생성시
    {
        int inventoryID = nextInventoryID++;
        inventories[inventoryID] = inventory;
        return inventoryID;
    }

    public void AddItemToInventory(int inventoryID, ItemSO item, int quantity)     //아이템을 인벤토리에 저장하기 위한 메서드.
    {
        if (inventories.ContainsKey(inventoryID))
        {
            inventories[inventoryID].AddItem(item, quantity);
        }
    }

    public void TransferItem(int fromInventoryID, int toInventoryID, ItemSO item, int quantity)   // 인벤토리간 아이템 교환
    {
        if (inventories.ContainsKey(fromInventoryID) && inventories.ContainsKey(toInventoryID))
        {
            if (inventories[fromInventoryID].RemoveItem(item, quantity))
            {
                inventories[toInventoryID].AddItem(item, quantity);
            }
        }
    }

    // 기타 메서드...
}