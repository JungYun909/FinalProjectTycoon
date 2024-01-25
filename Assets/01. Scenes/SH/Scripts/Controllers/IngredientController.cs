using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientController : MonoBehaviour, IInteractable
{
    public IngredientData _ingredientData;
    public GameObject moveFunction;
    
    private void Start()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = _ingredientData.sprite;
        
        if(!_ingredientData.canMove)
            return;
            
        moveFunction.SetActive(true);
        moveFunction.GetComponent<MovementController>().Move(_ingredientData.destination, _ingredientData);
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
        switch (_ingredientData.name)
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
        if(gameObject.GetComponent<IInteractable>() != null)
            gameObject.GetComponent<IInteractable>().OnColliderInteract();
        
        if(other.gameObject.GetComponent<IInteractable>() != null)
            other.gameObject.GetComponent<IInteractable>().OnColliderInteract();

        switch (_ingredientData.tag)
        {
            case "Dough":
                if (other.gameObject.GetComponent<Inventory>() != null)
                {
                    Debug.Log("in");
                    other.gameObject.GetComponent<Inventory>().AddDough(_ingredientData);
                    other.gameObject.GetComponent<InstallationController>()._installationData.doughContainer.Enqueue(gameObject);
                }
                break;
            default:
                other.gameObject.GetComponent<Inventory>().AddIngredient(_ingredientData);
                break;
        }
    }
}
