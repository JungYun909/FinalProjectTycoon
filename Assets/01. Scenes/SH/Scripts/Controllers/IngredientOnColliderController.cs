using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientOnColliderController : MonoBehaviour
{
    private IngredientController _ingredientController;

    private void Start()
    {
        _ingredientController = GetComponent<IngredientController>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(gameObject.GetComponent<IInteractable>() != null)
            gameObject.GetComponent<IInteractable>().OnColliderInteract();
        
        if(other.gameObject.GetComponent<IInteractable>() != null)
            other.gameObject.GetComponent<IInteractable>().OnColliderInteract();

        switch (_ingredientController._ingredientData.name)
        {
            case "Dough":
                if (other.gameObject.GetComponent<Inventory>() != null)
                {
                    Debug.Log("in");
                    other.gameObject.GetComponent<Inventory>().AddDough(_ingredientController._ingredientData);
                    other.gameObject.GetComponent<InstallationController>()._installationData.doughContainer.Enqueue(gameObject);
                }
                break;
            default:
                other.gameObject.GetComponent<Inventory>().AddIngredient(_ingredientController._ingredientData);
                break;
        }
    }
}
