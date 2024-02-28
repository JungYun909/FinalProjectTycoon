using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System;

[System.Serializable]
public class InventoryItemEntry
{
    public ItemSO item;
    public int quantity;
    public TextMeshPro qtyText;
    public Sprite itemIcon;

    public InventoryItemEntry(ItemSO item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}


public class AbstractInventory : MonoBehaviour
{
    private AbstractInventory inventory;
    public InventoryShow inventoryShow;
    public StandInventoryUI standInventory;
    public InstallationInventoryController inventoryController;
    public int maxDoughQuantity = 5; // 기본값으로 5를 설정


    public static event Action<AbstractInventory> OnInventoryClicked;
    public static event Action<MachineSO> DeliverMachineSO;

    public InstallationController controller;
    public GameObject ingredientPrefab;

    public Dictionary<ItemSO, int> Items { get;  set; } = new Dictionary<ItemSO, int>();
    public Dictionary<MachineSO, int> machines { get; set; } = new Dictionary<MachineSO, int>();

    public Queue<ItemSO> itemQueue = new Queue<ItemSO>();

    [SerializeField]
    private List<InventoryItemEntry> itemsListForInspector = new List<InventoryItemEntry>();
    public int inventoryID;


    public void CopyItemsToDoughContainer()
    {
        List<ItemSO> itemToDelete = new List<ItemSO>();
        if (inventoryID != 1000 && (controller == null || controller.doughContainer == null))
        {
            return;
        }
        GameObject destObj = controller.gameObject;
        foreach (ItemSO item in itemQueue)
        {
            GameManager.instance.spawnManager.SpawnIngredient(controller.gameObject, destObj, item);
            itemToDelete.Add(item);
        }
        foreach (var item in itemToDelete)
        {
            GameManager.instance.inventoryManager.RemoveItemFromInventory(inventoryID, item, 1);
        }
    }

    public void InitializeInventory(int loadedInventoryID)
    {
        if (loadedInventoryID != -1)
        {
            this.inventoryID = loadedInventoryID;
            GameManager.instance.inventoryManager.RegisterInventory(this);
            InventoryData myData = GameManager.instance.inventoryManager.GetInventoryDataById(loadedInventoryID);
            if(myData!= null)
            {
                LoadInventory(myData);
            }
            if(controller != null)
            {
                controller.installationFuctionSet += OpenUI;
            }
        }
    }

    public void InitSet()
    {
        controller.installationFuctionSet += OpenUI;
    }
    
    public void UpdateInspectorList()
    {
        itemsListForInspector.Clear();
        foreach (var item in Items)
        {
            itemsListForInspector.Add(new InventoryItemEntry(item.Key, item.Value));
        }
    }

    private void OpenUI()
    {

        if(controller!=null && controller._installationData != null)
        {
            if(controller._installationData.id == 5)
            {
                OpenStandInventoryUI();
            }
            else
            {
                OpenInventoryUI();
            }
        }
    }

    private void OpenStandInventoryUI()
    {
        GameManager.instance.uiManager.OpenWindow(standInventory, this);
        OnInventoryClicked?.Invoke(this);
    }
    private void OpenInventoryUI()
    {
        GameManager.instance.uiManager.OpenWindow(inventoryShow, this);
        OnInventoryClicked?.Invoke(this);
        DeliverMachineSO?.Invoke(controller._installationData);
    }

    public void LoadInventory(InventoryData data)
    {
        this.inventoryID = data.inventoryID;
        this.Items.Clear();

        if(this.inventoryID != 1000)
        {
            itemQueue.Clear();
        }

        foreach (var itemData in data.items)
        {
            ItemSO item = GameManager.instance.inventoryManager.itemDatabase.GetItemByID(itemData.itemID);
            if(item!=null)
            {
                this.Items[item] = itemData.quantity;
                if(this.inventoryID != 1000 && (item.id == 1 || item.type ==2))
                {
                    for(int i = 0;i < itemData.quantity; i++)
                    {
                        itemQueue.Enqueue(item);
                    }
                }
                if(this.inventoryID != 1000 && item.id!=1 && item.type ==1)
                {
                    controller.ingredients.Enqueue(item);
                }
            }
        }

        foreach (var machineData in data.machines)
        {
            MachineSO machine = GameManager.instance.inventoryManager.machineDatabase.GetItemByID(machineData.machineID);
            if(machine!=null)
            {
                this.machines[machine] = machineData.machineQuantity;
            }
        }

        foreach(var queueData in data.queueData) // poolManager spawnfromPool 이용?
        {
            ItemSO itemData = GameManager.instance.inventoryManager.itemDatabase.GetItemByID(queueData.itemSOID);
            if (itemData != null)
            {
                GameObject spawnedObj = GameManager.instance.poolManager.SpawnFromPool(ingredientPrefab);
                IngredientController ingredientController = spawnedObj.GetComponent<IngredientController>();
                ingredientController.itemData = itemData;
                ingredientController.interactInstallation = new Queue<float>(queueData.interactInstallationData);
                if (controller.doughContainer == null)
                    Debug.Log("No Dough Container");
                controller.doughContainer.Enqueue(spawnedObj);
                spawnedObj.SetActive(false);
            }
        }
    }
}
