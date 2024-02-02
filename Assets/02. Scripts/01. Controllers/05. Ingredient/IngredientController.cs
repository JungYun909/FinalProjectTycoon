using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class IngredientController : MonoBehaviour, IInteractable
{
    public ItemSO itemData;
    public Queue<float> interactInstallation = new Queue<float>();
    
    public GameObject moveFunction;
    public GameObject destination;

    public MovementController movementController;
    public TestSA addImageController;
    public SpriteRenderer spriteController;
    
    private void Start()
    {
        InitSet();
    }

    public void InitSet()
    {
        spriteController.sprite = itemData.sprite;
        
        if (itemData.canMove)
        {
            moveFunction.SetActive(true);
            movementController.speed = itemData.moveSpeed;
            movementController.destinationObj = destination;
        }
        
        addImageController.InitSetting();
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
        InstallationController controller;
        
        if(destination != other.gameObject)
            return;
        
        if(gameObject.GetComponent<IInteractable>() != null)
            gameObject.GetComponent<IInteractable>().OnColliderInteract();

        if (other.gameObject.GetComponent<IInteractable>() != null)
        {
            controller = other.gameObject.GetComponent<InstallationController>();
            controller.OnColliderInteract();
            
            if(itemData.tag == "Dough")
                controller.doughContainer.Enqueue(gameObject);
            else
                controller.ingredients.Enqueue(itemData);
        }

        if (other.gameObject.GetComponent<ShopInventory>() != null)
        {
            AbstractInventory shopInventory = other.gameObject.GetComponent<ShopInventory>();
            GameManager.instance.inventoryManager.AddItemToInventory(shopInventory.inventoryID, itemData, 1);
            return;
        }
        if (other.gameObject.GetComponentInChildren<AbstractInventory>() == null)
            return;

        AbstractInventory inventory = other.gameObject.GetComponentInChildren<AbstractInventory>();
        GameManager.instance.inventoryManager.AddItemToInventory(inventory.inventoryID, itemData, 1);
    }

    public void VisitIngredientDataSet(InstallationController controller, ItemSO ingredientData)
    {
        if (controller._installationData.haveIngredientInventory)
        {
            ItemSO ingredientItemData = controller.ingredients.Dequeue();
            interactInstallation.Enqueue(controller._installationData.id + (ingredientItemData.id * 0.1f));
            addImageController.AddImage(ingredientItemData.sprite);
            
            Debug.Log(controller._installationData.id + (ingredientItemData.id * 0.1f));
        }
    }

    public void VisitInstallationSet(InstallationController controller)
    {
        interactInstallation.Enqueue(controller._installationData.id);
    }
}
