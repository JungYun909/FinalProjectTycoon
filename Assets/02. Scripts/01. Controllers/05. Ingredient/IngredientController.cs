using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class IngredientController : MonoBehaviour, IInteractable
{
    public ItemSO itemData;
    public GameObject moveFunction;
    public GameObject destination;
    
    private void Start()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = itemData.sprite;

        if (itemData.canMove)
        {
            moveFunction.SetActive(true);
            MovementController controller = moveFunction.GetComponent<MovementController>();
            controller.speed = itemData.moveSpeed;
            controller.destinationObj = destination;
        }
    }
    
    public bool Continuous()
    {
        return false;
    }
    public void OnClickInteract()
    {
        return;
    }
    public void OffClickInteract()
    {
        return;
    }

    public void OnColliderInteract()
    {
        switch (itemData.tag)
        {
            case "Dough":
                gameObject.SetActive(false);
                break;
            default:
                PoolManager.instacne.DeSpawnFromPool(gameObject);
                break;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag != "Installation")
            return;
        
        if(gameObject.GetComponent<IInteractable>() != null)
            gameObject.GetComponent<IInteractable>().OnColliderInteract();
        
        if(other.gameObject.GetComponent<IInteractable>() != null)
            other.gameObject.GetComponent<IInteractable>().OnColliderInteract();

        InstallationController controller = other.gameObject.GetComponent<InstallationController>();

        if (other.gameObject.GetComponentInChildren<AbstractInventory>() == null)
            return;

        AbstractInventory inventory = other.gameObject.GetComponentInChildren<AbstractInventory>();
        
        InventoryManager.instance.AddItemToInventory(inventory.inventoryID, itemData, 1);
    }
}
