using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientController : MonoBehaviour, IInteractable
{
    public IngredientData _ingredientData;
    public GameObject moveFunction;
    public GameObject destination;
    
    private void Start()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = _ingredientData.sprite;

        if (_ingredientData.canMove)
        {
            moveFunction.SetActive(true);
            MovementController controller = moveFunction.GetComponent<MovementController>();
            controller.speed = _ingredientData.moveSpeed;
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

    public void OnColliderInteract()
    {
        switch (_ingredientData.tag)
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
        
        switch (_ingredientData.tag)
        {
            case "Dough":
                if (controller._installationData.haveDoughInventory)
                {
                    other.gameObject.GetComponentInChildren<Inventory>().AddDough(_ingredientData);
                    controller.doughContainer.Enqueue(gameObject);
                }
                break;
            default:
                if (controller._installationData.haveIngredientInventory)
                    other.gameObject.GetComponentInChildren<Inventory>().AddIngredient(_ingredientData);
                break;
        }
    }
}
