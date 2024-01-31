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
    public int maxDoughQuantity = 5; // 기본값으로 5를 설정

    public static event Action<AbstractInventory> OnInventoryClicked;

    public InstallationController controller;



    public Dictionary<ItemSO, int> Items { get;  set; } = new Dictionary<ItemSO, int>();

    [SerializeField]
    private List<InventoryItemEntry> itemsListForInspector = new List<InventoryItemEntry>();
    public int inventoryID;

    protected virtual void Start()
    {
        inventoryID = GameManager.instance.inventoryManager.RegisterInventory(this);
    }

    private void OnEnable()
    {
        if (controller != null)
        {
            controller.installationFuctionSet += OpenInventoryUI;
            OpenInventoryUI();
            GameManager.instance.uiManager.CloseAll();
        }
    }

    public void InitSet()
    {
        controller.installationFuctionSet += OpenInventoryUI;
    }
    
    public void UpdateInspectorList()
    {
        itemsListForInspector.Clear();
        foreach (var item in Items)
        {
            itemsListForInspector.Add(new InventoryItemEntry(item.Key, item.Value));
        }
    }

    private void OpenInventoryUI()
    {
        GameManager.instance.uiManager.OpenWindow(inventoryShow, this);
        OnInventoryClicked?.Invoke(this);
    }

}
