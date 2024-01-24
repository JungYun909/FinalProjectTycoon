using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientController : MonoBehaviour, IInteractable
{
    public IngredientData _ingredientData;

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
    private void Start()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = _ingredientData.sprite;
    }

    
    private void OnCollisionEnter2D(Collision2D other)
    {
        // if (gameObject.GetComponent<IInteractable>() != null)
        // {
        //     gameObject.GetComponent<IInteractable>().OnColliderInteract();
        //     _ingredientData.stat.VisitGameObjects.Add(other.gameObject);
        // }
        //
        // if (other.gameObject.GetComponent<IInteractable>() != null && other.gameObject.GetComponent<InstallationData>() != null)
        // {
        //     if(gameObject.tag == "Dough")
        //     {
        //         if(other.gameObject.GetComponent<InstallationData>().stat.curInventoryItem == null)
        //         {
        //             other.gameObject.GetComponent<InstallationData>().stat.curInventoryItem = gameObject;
        //             other.gameObject.GetComponent<IInteractable>().OnColliderInteract();
        //         }
        //     }
        //     else if(gameObject.tag == "Resource")
        //     {
        //         if(other.gameObject.GetComponent<InstallationData>().stat.curIngredientInventoryItem == null)
        //         {
        //             other.gameObject.GetComponent<InstallationData>().stat.curIngredientInventoryItem = gameObject;
        //             other.gameObject.GetComponent<IInteractable>().OnColliderInteract();
        //         }
        //     }
        // }
    }
}
