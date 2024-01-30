using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeIngredientToFood : MonoBehaviour
{
    public IngredientController controller;
    public InstallationDestinationController desController;

    private int recipeIndex;

    public void FoodMaker()
    {
        for (int i = controller.interactInstallation.Count - 1; i >= 0; i--)
        {
            recipeIndex += (int)Math.Pow(10, i) * controller.interactInstallation.Dequeue();
        }

        int spawnFoodID = GameManager.instance.recipeManager.CompareWithResipe(recipeIndex);
        GameManager.instance.spawnManager.SpawnIngredient(gameObject, desController.destination[1], GameManager.instance.dataManager.ingredientSub[spawnFoodID - 1]);
    }
}
