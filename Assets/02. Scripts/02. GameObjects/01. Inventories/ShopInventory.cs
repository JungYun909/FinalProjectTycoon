using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEntry
{
    public ItemSO item;
    public int quantity;
}    //인벤토리 인스펙터로 검토하기 위한 필드, 추후 삭제 예정

public class ShopInventory : AbstractInventory
{
    public UIBase inventoryUI;
    private void OnEnable()
    {
        GameManager.instance.inventoryManager.InventoryLoadDone += LoadPlayerInventory;
    }

    private void OnDisable()
    {
        GameManager.instance.inventoryManager.InventoryLoadDone -= LoadPlayerInventory;
    }

    private void LoadPlayerInventory()
    {
        InventoryData myData = GameManager.instance.inventoryManager.GetInventoryDataById(1000);
        if(myData != null)
            LoadInventory(myData);
        GameManager.instance.inventoryManager.RegisterInventory(this);
        inventoryID = 1000;
    }
}