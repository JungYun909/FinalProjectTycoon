using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class InventoryData
{
    public int inventoryID;
    public List<ItemData> items; // 아이템 데이터 리스트
    public List<MachineData> machines;
    public List<QueueData> queueData;

    public InventoryData(int inventoryID)
    {
        this.inventoryID = inventoryID;
        this.items = new List<ItemData>();
        this.machines = new List<MachineData>();
        this.queueData = new List<QueueData>();
    }
}
[System.Serializable]
public class ItemData
{
    public int itemID; // ItemSO의 id와 매칭
    public int quantity; // 해당 아이템의 수량

    public ItemData(int itemID, int quantity)
    {
        this.itemID = itemID;
        this.quantity = quantity;
    }
}
[System.Serializable]
public class MachineData
{
    public int machineID;
    public int machineQuantity;
    public MachineData(int machineID, int quantity)
    {
        this.machineID= machineID;
        this.machineQuantity = quantity;
    }
}

[System.Serializable]
public class QueueData
{
    public int itemSOID;
    public List<float> interactInstallationData;

    public QueueData(int itemSOID, List<float>interactInstallationData)
    {
        this.itemSOID = itemSOID;
        this.interactInstallationData = new List<float>(interactInstallationData);
    }
}

[System.Serializable]
public class InventoryWrapper
{
    public int nextInventoryID;
    public List<InventoryData> allInventories;
}


public class InventoryManager : MonoBehaviour
{
    public List<InventoryData> allInventories = new List<InventoryData>();
    public event Action<int> OnInventoryUpdated; // 인벤토리 ID를 인자로 사용

    public ItemDatabaseSO itemDatabase;  // 아이템 데이터베이스 참조
    public MachineDatabaseSO machineDatabase;
    public Dictionary<int, AbstractInventory> inventories = new Dictionary<int, AbstractInventory>();   // 인벤토리를 딕셔너리로 정리
    public int nextInventoryID = 1001;   // 인덱스용 아이디를 부여하기 위한 필드
    private int playerInventoryID = 1000;
    private int inventoryID;

    public event Action InventoryLoadDone;

    public  void LoadInventoryData()
    {
        InventoryWrapper loadedData = GameManager.instance.dataManager.LoadAllInventories();
        nextInventoryID = loadedData.nextInventoryID;
        allInventories = loadedData.allInventories;
        InventoryLoadDone?.Invoke();
    }

    public int RegisterInventory(AbstractInventory inventory)    // 새로 생기는 인벤토리를 딕셔너리에 등재하기 위한 메서드. 모든 인벤토리 생성시
    {

        if(inventory is ShopInventory)
        {
            inventoryID = 1000;
        }
        else if (inventory.inventoryID != 0)
        {
            inventoryID = inventory.inventoryID;
        }
        else { inventoryID = nextInventoryID++; }
        inventories[inventoryID] = inventory;
        InventoryData existingData = GetInventoryDataById(inventoryID);
        if(existingData == null)
        {
            InventoryData newData = new InventoryData(inventoryID);
            allInventories.Add(newData);
        }
        return inventoryID;
    }

    public InventoryData GetInventoryDataById(int inventoryID)
    {
        return allInventories.Find(inventory => inventory.inventoryID == inventoryID);
    }

    public void DeleteInventoryData(int inventoryID)
    {
        InventoryData inventoryToDelete = GetInventoryDataById(inventoryID);
        if (inventoryToDelete != null)
        {
            allInventories.Remove(inventoryToDelete);
            if (inventories.ContainsKey(inventoryID))
            {
                inventories.Remove(inventoryID);
            }
        }
    }

    public void AddItemToInventory(int inventoryID, ItemSO item, int quantity)
    {
        if (inventories.ContainsKey(inventoryID))
        {
            AbstractInventory inventory = inventories[inventoryID];
            if(!inventory.Items.ContainsKey(item))
            {
                inventory.Items[item] = 0;
            }
            inventory.Items[item] += quantity;
            if(!(inventoryID == 1000 && (item.id == 1 || item.type ==2)))
            {
                if (item.id == 1 || item.type == 2)
                {
                    for (int i = 0; i < quantity; i++)
                    {
                        inventory.itemQueue.Enqueue(item);
                    }
                }
                else if (item.id != 1 && (item.type == 1 || item.type ==4))
                {
                    if (inventory.controller != null)
                    {
                        for (int i = 0; i < quantity; i++)
                        {
                            inventory.controller.ingredients.Enqueue(item);
                        }
                    }
                }
            }
            ReviseAllInventoriesListForItem(inventoryID, item, quantity);
            OnInventoryUpdated?.Invoke(inventoryID); // 이벤트 발생
            inventory.UpdateInspectorList();
        }
    }
    public void ReviseAllInventoriesListForItem(int inventoryID, ItemSO item, int quantity)
    {
        InventoryData inventoryData = CheckInventoryIDInAllInventoriesList(inventoryID);
        if(inventoryData != null)
        {
            if(inventoryData.items != null)
            {
                for (int i = 0; i < inventoryData.items.Count; i++)
                {
                    if (inventoryData.items[i].itemID == item.id)
                    {
                        inventoryData.items[i].quantity += quantity;

                        if (inventoryData.items[i].quantity <= 0)
                        {
                            inventoryData.items.RemoveAt(i);
                        }
                        return;
                    }
                }
                if (quantity > 0)
                {
                    inventoryData.items.Add(new ItemData(item.id, quantity));
                }
            }
        }
    }

    public void ReviseAllInventoriesListForMachine(int inventoryID, MachineSO machine, int quantity)
    {
        InventoryData inventoryData = CheckInventoryIDInAllInventoriesList(inventoryID);
        if (inventoryData != null)
        {
            if(inventoryData.machines!=null)
            {
                for (int i = 0; i < inventoryData.machines.Count; i++)
                {
                    if (inventoryData.machines[i].machineID == machine.id)
                    {
                        inventoryData.machines[i].machineQuantity += quantity;

                        if (inventoryData.machines[i].machineQuantity <= 0)
                        {
                            inventoryData.machines.RemoveAt(i);
                        }
                        return;
                    }
                }
                if (quantity > 0)
                {
                    inventoryData.machines.Add(new MachineData(machine.id, quantity));
                }
            }
        }
    }

    public void AddMachineToPlayerInventory(MachineSO machine, int quantity)
    {
        if(inventories.ContainsKey(1000))
        {
            var inventory = inventories[1000];
            if(!inventory.machines.ContainsKey(machine))
            {
                inventory.machines[machine] = 0;
            }
            inventory.machines[machine] += quantity;

            ReviseAllInventoriesListForMachine(1000, machine, quantity);
            OnInventoryUpdated?.Invoke(1000);
            inventory.UpdateInspectorList();
        }
    }

    private InventoryData CheckInventoryIDInAllInventoriesList(int inventoryID)
    {
        InventoryData inventoryData = null;
        foreach (var inventory in allInventories)
        {
            if (inventory.inventoryID == inventoryID)
            {
                inventoryData = inventory;
                break;
            }
        }
        if (inventoryData == null)
        {
            inventoryData = new InventoryData(inventoryID);
            allInventories.Add(inventoryData);
        }
        return inventoryData;
    }

    public bool RemoveMachineFromPlayerInventory(MachineSO machine, int quantity)
    {
        var inventory = inventories[1000];
        if(inventory.machines.ContainsKey(machine) && inventory.machines[machine] >= quantity)
        {
            inventory.machines[machine] -= quantity;
            if(inventory.machines[machine] == 0)
            {
                inventory.machines.Remove(machine);
            }
            ReviseAllInventoriesListForMachine(1000, machine, -quantity);

            OnInventoryUpdated?.Invoke(1000); // 이벤트 발생
            inventory.UpdateInspectorList();
            return true;
        }
        return false;
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
                ReviseAllInventoriesListForItem(inventoryID, item, -quantity);
                OnInventoryUpdated?.Invoke(inventoryID); // 이벤트 발생
                inventory.UpdateInspectorList();

                return true;
            }
        }
        return false;
    }

    public void UpdateInventoryAfterDequeue(int inventoryID, ItemSO itemSO)
    {
        InventoryData inventoryData = GetInventoryDataById(inventoryID);
        if(inventoryData == null)
        {
            return;
        }

        QueueData queueDataToRemove = null;
        foreach (var queueData in inventoryData.queueData)
        {
            if(queueData.itemSOID == itemSO.id)
            {
                queueDataToRemove = queueData;
                break;
            }
        }
        if(queueDataToRemove != null)
        {
            inventoryData.queueData.Remove(queueDataToRemove);
        }

        ItemData itemDataToUpdate = inventoryData.items.Find(itemData => itemData.itemID == itemSO.id);
        if(itemDataToUpdate != null)
        {
            itemDataToUpdate.quantity -= 1;
            if(itemDataToUpdate.quantity<=0)
            {
                inventoryData.items.Remove(itemDataToUpdate);
            }
        }
    }

    public void TransferItem(int fromInventoryID, int toInventoryID, ItemSO item, int quantity)
    {
        if(RemoveItemFromInventory(fromInventoryID, item, quantity))
        {
            AddItemToInventory(toInventoryID, item, quantity);
            AbstractInventory fromInventory = inventories[fromInventoryID];
            if (fromInventory.controller != null)
                fromInventory.controller.ingredients.Clear();
            else
                return;
        }
    }

    //public void ExchangeItems(int fromInventoryID, int toInventoryID, ItemSO itemFromFromInventory, ItemSO itemFromToInventory, int quantityOfFromInventoryItem, int quantityOfToInventoryItem)
    //{
    //    TransferItem(fromInventoryID, toInventoryID, itemFromFromInventory, quantityOfFromInventoryItem);
    //    TransferItem(toInventoryID, fromInventoryID, itemFromToInventory, quantityOfToInventoryItem)
    //}

    public IEnumerator SaveAllInventoriesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            GameManager.instance.dataManager.SaveInventoryData(nextInventoryID, allInventories);
        }
    }
}