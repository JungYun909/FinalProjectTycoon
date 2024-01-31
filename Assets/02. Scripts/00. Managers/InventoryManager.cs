using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public event Action<int> OnInventoryUpdated; // 인벤토리 ID를 인자로 사용

    public ItemDatabaseSO itemDatabase;  // 아이템 데이터베이스 참조
    private Dictionary<int, AbstractInventory> inventories = new Dictionary<int, AbstractInventory>();   // 인벤토리를 딕셔너리로 정리
    private int nextInventoryID = 1000;   // 인덱스용 아이디를 부여하기 위한 필드

    public int RegisterInventory(AbstractInventory inventory)    // 새로 생기는 인벤토리를 딕셔너리에 등재하기 위한 메서드. 모든 인벤토리 생성시
    {
        int inventoryID = nextInventoryID++;
        inventories[inventoryID] = inventory;
        return inventoryID;
    }

    public void AddItemToInventory(int inventoryID, ItemSO item, int quantity)
    {
        if (inventories.ContainsKey(inventoryID))
        {
            var inventory = inventories[inventoryID];

            // 반죽 아이템의 경우
            if (item.id == 1)
            {
                inventory.Items.TryGetValue(item, out int currentQuantity);

                // 새로운 수량 계산
                int newQuantity = currentQuantity + quantity;

                // 최대 수량을 초과하는 경우, 최대 수량으로 제한
                if (newQuantity > inventory.maxDoughQuantity)
                {
                    newQuantity = inventory.maxDoughQuantity;
                }

                // 수량 업데이트
                inventory.Items[item] = newQuantity;
            }
            else
            {
                // 다른 아이템의 경우, 정상적으로 추가
                if (!inventory.Items.ContainsKey(item))
                {
                    inventory.Items[item] = 0;
                }
                inventory.Items[item] += quantity;
            }

            OnInventoryUpdated?.Invoke(inventoryID); // 이벤트 발생
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
                OnInventoryUpdated?.Invoke(inventoryID); // 이벤트 발생
                inventory.UpdateInspectorList();

                return true;
            }
        }
        return false;
    }

    public void TransferItem(int fromInventoryID, int toInventoryID, ItemSO item, int quantity)
    {
        if (RemoveItemFromInventory(fromInventoryID, item, quantity))
        {
            AddItemToInventory(toInventoryID, item, quantity);
        }
        //    if (inventories.ContainsKey(fromInventoryID) && inventories.ContainsKey(toInventoryID))
        //    {
        //        var fromInventory = inventories[fromInventoryID].Items;
        //        var toInventory = inventories[toInventoryID].Items;

        //        // 아이템을 이동시킬 수 있는지 확인
        //        if (fromInventory.ContainsKey(item) && fromInventory[item] >= quantity)
        //        {
        //            // 아이템을 기존 인벤토리에서 제거
        //            fromInventory[item] -= quantity;
        //            if (fromInventory[item] == 0)
        //            {
        //                fromInventory.Remove(item);
        //            }

        //            // 아이템을 새 인벤토리에 추가
        //            if (!toInventory.ContainsKey(item))
        //            {
        //                toInventory[item] = 0;
        //            }
        //            toInventory[item] += quantity;

        //            OnInventoryUpdated?.Invoke(fromInventoryID); // 이벤트 발생
        //            OnInventoryUpdated?.Invoke(toInventoryID); // 이벤트 발생

        //            // 인벤토리 UI 업데이트
        //        }
        //    }
    }
    public void TransformItem(int inventoryID, ItemSO fromItem, ItemSO toItem, float duration)
    {
        StartCoroutine(TransformAfterSomeSceonds(inventoryID, fromItem, toItem, duration));
    }

    // 지정된 시간 후에 아이템을 변환하는 코루틴
    private IEnumerator TransformAfterSomeSceonds(int inventoryID, ItemSO fromItem, ItemSO toItem, float duration)
    {
        yield return new WaitForSeconds(duration);

        if (inventories.ContainsKey(inventoryID) && inventories[inventoryID].Items.ContainsKey(fromItem))
        {
            var inventory = inventories[inventoryID];
            int fromItemQuantity = inventory.Items[fromItem];
            inventory.Items.Remove(fromItem);

            if (!inventory.Items.ContainsKey(toItem))
            {
                inventory.Items[toItem] = 0;
            }
            inventory.Items[toItem] += fromItemQuantity;

            inventory.UpdateInspectorList();
        }
    }
}