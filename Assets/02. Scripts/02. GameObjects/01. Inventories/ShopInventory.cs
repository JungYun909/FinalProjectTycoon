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
    protected override void Start()
    {
        base.Start();
        if (GameManager.instance.dataManager.IsFileExist(1000))
        {
            LoadInventory(GameManager.instance.dataManager.LoadInventoryData(1000));
        }
        AddTestItem1();
        AddTestItem2();
        AddTestItem3();
        AddTestItem4();
        AddTestItem5();
        AddTestItem6();
    }
    //private Dictionary<ItemSO, int> items = new Dictionary<ItemSO, int>();

    public void AddTestItem1() { AddItemThroughManager(1); }
    public void AddTestItem2() { AddItemThroughManager(2); }
    public void AddTestItem3() { AddItemThroughManager(1000); }
    public void AddTestItem4() { AddItemThroughManager(1001); }
    public void AddTestItem5() { AddItemThroughManager(1002); }
    public void AddTestItem6() { AddItemThroughManager(1003); }
    public void AddTestItem7() { AddItemThroughManager(7); }
    public void AddTestItem8() { AddItemThroughManager(8); }
    public void AddTestItem9() { AddItemThroughManager(9); }
    public void AddTestItem10() { AddItemThroughManager(10); }
    public void AddTestItem11() { AddItemThroughManager(11); }

    private void AddItemThroughManager(int itemID)
    {
        ItemSO item = GameManager.instance.inventoryManager.itemDatabase.GetItemByID(itemID);
        if (item != null)
        {
            GameManager.instance.inventoryManager.AddItemToInventory(inventoryID, item, 5);
        }
    }
}