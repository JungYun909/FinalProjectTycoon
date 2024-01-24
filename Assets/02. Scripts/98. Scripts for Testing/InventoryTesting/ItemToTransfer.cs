using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToTransfer : MonoBehaviour
{
    public ItemSO itemInfo;
    public InventoryManager inventoryManager;

    private void Awake()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AbstractInventory inventory = collision.GetComponent<AbstractInventory>();
        if(inventory != null)
        {
            inventoryManager.AddItemToInventory(inventory.inventoryID, itemInfo, 1);
            Destroy(gameObject);
	    }
        else { Debug.Log("NO!"); }
    }
}
