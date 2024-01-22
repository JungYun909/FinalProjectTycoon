using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInventory : MonoBehaviour
{
    public Dictionary<ItemSO, int> Items { get;  set; } = new Dictionary<ItemSO, int>();

    protected InventoryManager inventoryManager;
    public int inventoryID;

    protected virtual void Awake()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        inventoryID = inventoryManager.RegisterInventory(this);
    }

    // 필요한 경우 데이터 접근 메서드 추가
}