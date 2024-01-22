using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryPlayer : MonoBehaviour
{
    public float speed = 5.0f;
    private InventoryManager inventoryManager;
    private ShopInventory inventory;

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        inventory = FindObjectOfType<ShopInventory>();          // 출발 오브젝트의 인벤토리를 찾아오도록 함  
    }

    private void Update()
    {
        Vector2 moveDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection.x += 1;
        }

        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)         // 추후 OnInteract와 연관되어야 하지 않을지.. 관련 내용을 InventoryManager에서 다루도록 해야 .
    {
        if (other.CompareTag("ObjectForTest"))             // 현재는 태그를 찾고 있으나 해당 오브젝트가 부딪힌 오브젝트의 Inventory 소유 여부를 확인하고 그걸 TransferItem 메서드에 사용할 방법을 찾아야
        {
            int playerInventoryID = inventory.inventoryID;    // 이것 역시 현재는 테스트용으로 플레이어 인벤토리지만 나중에는 출발한 오브젝트의 인벤토리 아이디를 가져~  
            ItemSO itemToRemove = inventoryManager.itemDatabase.GetItemByID(1);
            ItemSO itemToAdd = inventoryManager.itemDatabase.GetItemByID(11);

            if (inventoryManager.RemoveItemFromInventory(playerInventoryID, itemToRemove, 1))
            {
                inventoryManager.AddItemToInventory(playerInventoryID, itemToAdd, 1);
            }
        }
    }
}