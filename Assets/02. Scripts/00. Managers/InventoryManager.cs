using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public event Action<int> OnInventoryUpdated; // 인벤토리 ID를 인자로 사용

    public ItemDatabaseSO itemDatabase;  // 아이템 데이터베이스 참조
    private Dictionary<int, AbstractInventory> inventories = new Dictionary<int, AbstractInventory>();   // 인벤토리를 딕셔너리로 정리
    private int nextInventoryID = 1001;   // 인덱스용 아이디를 부여하기 위한 필드
    private int playerInventoryID = 1000;

    public int RegisterInventory(AbstractInventory inventory)    // 새로 생기는 인벤토리를 딕셔너리에 등재하기 위한 메서드. 모든 인벤토리 생성시
    {
        int inventoryID;
        if(inventory is ShopInventory)
        {
            inventoryID = playerInventoryID;
        }
        else
        {
            inventoryID = nextInventoryID++;
        }
        inventories[inventoryID] = inventory;
        return inventoryID;
    }

    public void AddItemToInventory(int inventoryID, ItemSO item, int quantity)
    {
        if (inventories.ContainsKey(inventoryID))
        {
            var inventory = inventories[inventoryID];
            
            if(!inventory.Items.ContainsKey(item))
            {
                inventory.Items[item] = 0;
            }
            inventory.Items[item] += quantity;

            if(item.id == 1 || item.type ==2)
            {
                if (inventoryID == 1000)
                {
                    return;
                }
                else
                {
                    for (int i = 0; i < quantity; i++)
                    {
                        inventory.itemQueue.Enqueue(item);
                    }
                }
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

                if(inventoryID!=1000 && (item.id == 1 || item.type ==2))
                {
                    for(int i = 0; i<quantity;i++)
                    {
                        if(inventory.itemQueue.Count > 0 && inventory.itemQueue.Peek() ==item)
                        {
                            inventory.itemQueue.Dequeue();
                        }
                    }
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
        if (inventories.ContainsKey(fromInventoryID) && inventories.ContainsKey(toInventoryID))
        {
            var fromInventory = inventories[fromInventoryID];
            var toInventory = inventories[toInventoryID];

            Debug.Log($"[TransferItem] Trying to transfer {quantity} of {item.name} from {fromInventoryID} to {toInventoryID}");

            // 아이템을 이동시킬 수 있는지 확인
            if (fromInventory.Items.ContainsKey(item) && fromInventory.Items[item] >= quantity)
            {
                Debug.Log($"[TransferItem] Enough items in fromInventory. Current quantity: {fromInventory.Items[item]}");

                // 기존 인벤토리에서 아이템 제거
                fromInventory.Items[item] -= quantity;
                Debug.Log($"[TransferItem] Items removed from fromInventory. New quantity: {fromInventory.Items[item]}");

                // 큐 관리
                if (item.id == 1 || item.type == 2)
                {
                    for (int i = 0; i < quantity; i++)
                    {
                        if (fromInventory.itemQueue.Count > 0 && fromInventory.itemQueue.Peek() == item)
                        {
                            fromInventory.itemQueue.Dequeue();
                            Debug.Log($"[TransferItem] Item dequeued from fromInventory's queue");
                        }
                    }
                }

                // 새 인벤토리에 아이템 추가
                if (!toInventory.Items.ContainsKey(item))
                {
                    toInventory.Items[item] = 0;
                }
                toInventory.Items[item] += quantity;
                Debug.Log($"[TransferItem] Items added to toInventory. New quantity: {toInventory.Items[item]}");

                // 새 인벤토리의 큐 관리
                if (item.id == 1 || item.type == 2)
                {
                    for (int i = 0; i < quantity; i++)
                    {
                        toInventory.itemQueue.Enqueue(item);
                        Debug.Log($"[TransferItem] Item enqueued to toInventory's queue");
                    }
                }

                // 이벤트 발생 및 UI 업데이트
                OnInventoryUpdated?.Invoke(fromInventoryID);
                OnInventoryUpdated?.Invoke(toInventoryID);
            }
            else
            {
                Debug.Log($"[TransferItem] Not enough items in fromInventory or item not found");
            }
        }
        else
        {
            Debug.Log($"[TransferItem] One or both inventories not found");
        }
    }

    public void TransformItem (int inventoryID, ItemSO fromItem, ItemSO toItem, float duration)
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