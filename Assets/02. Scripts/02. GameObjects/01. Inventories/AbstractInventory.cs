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
        controller.installationFuctionSet += OpenInventoryUI;

        OpenInventoryUI();
        GameManager.instance.uiManager.CloseAll();
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
        Debug.Log("Clicked!");
        Debug.Log("OnInventoryClicked, " + this.inventoryID);
        GameManager.instance.uiManager.OpenWindow(inventoryShow, this);
        OnInventoryClicked?.Invoke(this);
    }

    // 필요한 경우 데이터 접근 메서드 추가

    //public void UpdateInventoryUI(Dictionary<ItemSO, int> items)
    //{
    //    foreach (Transform slots in contentPanel)
    //    {
    //        Destroy(child.gameObject);
    //    }

    //    GameObject currentLine = null;
    //    int slotIndex = 0;

    //    foreach (var entry in items)
    //    {
    //        // 새로운 라인이 필요한 경우 생성
    //        if (slotIndex % 5 == 0)
    //        {
    //            currentLine = Instantiate(itemLinePrefab, contentPanel);
    //        }

    //        // 아이템 슬롯 생성 및 설정
    //        GameObject slotObject = Instantiate(itemSlotPrefab, currentLine.transform);
    //        ItemSlotInfo slotInfo = slotObject.GetComponent<ItemSlotInfo>();
    //        slotInfo.Setup(entry.Key, entry.Value);

    //        slotIndex++;
    //    }
    //}
}
