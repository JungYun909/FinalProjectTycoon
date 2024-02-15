using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToTransfer : MonoBehaviour
{
    public ItemSO itemInfo;
    public InventoryManager inventoryManager;
    public float movement = 2.0f;
    private void Awake()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)    // 현재는, 부딪힌 사물의 인벤토리에 직행. Destination 설정에서 타겟 Inventory넘버를 받아오도록 하고, 그 인벤토리 넘버가 부딪힌 넘ㅂ와 일치할 때 들어가는것으로 수정할 수 있음. (도중에 엉뚱한 인벤토리로 들어가는것 방지)
    {
        AbstractInventory inventory = collision.GetComponentInChildren<AbstractInventory>();
        if(inventory != null)
        {
            inventoryManager.AddItemToInventory(inventory.inventoryID, itemInfo, 1);
            Destroy(gameObject);
            Debug.Log("Entered");
	    }
        else { Debug.Log("NO!"); }
    }
}
