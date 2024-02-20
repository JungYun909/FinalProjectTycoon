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

    public event Action OnInteractEvent; 
    
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
        //if(itemData.id == 1)
        //{
        //    gameObject.SetActive(false);
        //    movementController.isMove = false;
        //}
        //else if (itemData.type ==2)
        //{
        //    GameManager.instance.poolManager.DeSpawnFromPool(gameObject);
        //    movementController.isMove = false;
        //}
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        InstallationController controller;
        if(destination != other.gameObject)
            return;

        if (gameObject.GetComponent<IInteractable>() != null)
        {
            gameObject.GetComponent<IInteractable>().OnColliderInteract();
            OnInteractEvent?.Invoke();
        }

        if (other.gameObject.GetComponent<IInteractable>() != null)
        {
            if (other.gameObject.GetComponent<InstallationController>().destinationID != 1)
            {
                controller = other.gameObject.GetComponent<InstallationController>();
                controller.OnColliderInteract();

                if (itemData.tag == "Dough")
                    controller.doughContainer.Enqueue(gameObject);
                else if (controller.destinationID == 1)
                    return;
                else
                    controller.ingredients.Enqueue(itemData);
            }
        }

        if (other.gameObject.GetComponent<ShopInventory>() != null)
        {
            GameManager.instance.inventoryManager.AddItemToInventory(1000, itemData, 1);
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
            Debug.Log(controller._installationData.id + (ingredientData.id * Mathf.Pow(0.1f,Mathf.FloorToInt(Mathf.Log10(ingredientData.id) + 1))));
            interactInstallation.Enqueue(controller._installationData.id + (ingredientData.id * Mathf.Pow(0.1f,Mathf.FloorToInt(Mathf.Log10(ingredientData.id) + 1))));
            addImageController.AddImage(ingredientData.sprite);
        }
    }

    public void VisitInstallationSet(InstallationController controller)
    {
        if(!controller._installationData.haveIngredientInventory)
            interactInstallation.Enqueue(controller._installationData.id);
    }
}
