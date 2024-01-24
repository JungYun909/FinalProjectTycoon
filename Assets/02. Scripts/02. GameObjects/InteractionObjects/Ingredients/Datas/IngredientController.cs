using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientController : MonoBehaviour
{
    public IngredientData _ingredientData;

    private void Awake()
    {
        _ingredientData.InitSetting();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (gameObject.GetComponent<IInteractable>() != null)
        {
            gameObject.GetComponent<IInteractable>().OnColliderInteract();
            _ingredientData.stat.VisitGameObjects.Add(other.gameObject);
        }

        if (other.gameObject.GetComponent<IInteractable>() != null && other.gameObject.GetComponent<InstallationData>() != null)
        {
            if(gameObject.tag == "Dough")
            {
                if(other.gameObject.GetComponent<InstallationData>().stat.curInventoryItem == null)
                {
                    other.gameObject.GetComponent<InstallationData>().stat.curInventoryItem = gameObject;
                    other.gameObject.GetComponent<IInteractable>().OnColliderInteract();
                }
            }
            else if(gameObject.tag == "Resource")
            {
                if(other.gameObject.GetComponent<InstallationData>().stat.curIngredientInventoryItem == null)
                {
                    other.gameObject.GetComponent<InstallationData>().stat.curIngredientInventoryItem = gameObject;
                    other.gameObject.GetComponent<IInteractable>().OnColliderInteract();
                }
            }
        }
    }
}
