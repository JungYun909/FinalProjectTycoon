using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class InventoryItemEntry
{
    public ItemSO item;
    public int quantity;

    public InventoryItemEntry(ItemSO item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}


public abstract class AbstractInventory : MonoBehaviour
{
    public Dictionary<ItemSO, int> Items { get;  set; } = new Dictionary<ItemSO, int>();

    [SerializeField]
    private List<InventoryItemEntry> itemsListForInspector = new List<InventoryItemEntry>();

    protected InventoryManager inventoryManager;
    public int inventoryID;

    protected virtual void Awake()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        inventoryID = inventoryManager.RegisterInventory(this);
    }


    public void UpdateInspectorList()
    {
        itemsListForInspector.Clear();
        foreach (var item in Items)
        {
            itemsListForInspector.Add(new InventoryItemEntry(item.Key, item.Value));
        }
    }
    // 필요한 경우 데이터 접근 메서드 추가
}