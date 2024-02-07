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
    public int maxDoughQuantity = 5; // 기본값으로 5를 설정


    public static event Action<AbstractInventory> OnInventoryClicked;

    public InstallationController controller;

    public Dictionary<ItemSO, int> Items { get;  set; } = new Dictionary<ItemSO, int>();
    public Queue<ItemSO> itemQueue = new Queue<ItemSO>();

    [SerializeField]
    private List<InventoryItemEntry> itemsListForInspector = new List<InventoryItemEntry>();
    public int inventoryID;

    protected virtual void Start()
    {
        inventoryID = GameManager.instance.inventoryManager.RegisterInventory(this);
        controller = GetComponentInParent<InstallationController>();
    }

    private void OnEnable()
    {
        if (controller != null)
        {
            controller.installationFuctionSet += OpenUI;
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
    }
}
