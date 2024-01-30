using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class IngredientController : MonoBehaviour, IInteractable
{
    public ItemSO itemData;
    public Queue<int> interactInstallation = new Queue<int>();
    
    public GameObject moveFunction;
    public GameObject destination;

    public MovementController movementController;
    
    private void Start()
    {
        InitSet();
    }

    public void InitSet()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = itemData.sprite;
        
        if (itemData.canMove)
        {
            moveFunction.SetActive(true);
            movementController.speed = itemData.moveSpeed;
            movementController.destinationObj = destination;
        }
    }

    private void OnDisable()
    {
        movementController.speed = 0;
        movementController.destinationObj = null;
        Debug.Log("지워졌다");
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
                movementController.isMove = false;
                break;
            default:
                GameManager.instance.poolManager.DeSpawnFromPool(gameObject);
                movementController.isMove = false;
                break;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(destination != other.gameObject)
            return;
        
        if(gameObject.GetComponent<IInteractable>() != null)
            gameObject.GetComponent<IInteractable>().OnColliderInteract();

        if (other.gameObject.GetComponent<IInteractable>() != null)
        {
            InstallationController controller = other.gameObject.GetComponent<InstallationController>();
            controller.OnColliderInteract();
            interactInstallation.Enqueue(controller._installationData.id);
        }

        if (other.gameObject.GetComponentInChildren<AbstractInventory>() == null)
            return;

        AbstractInventory inventory = other.gameObject.GetComponentInChildren<AbstractInventory>();
        other.gameObject.GetComponent<InstallationController>().doughContainer.Enqueue(gameObject);
        GameManager.instance.inventoryManager.AddItemToInventory(inventory.inventoryID, itemData, 1);
    }
}
