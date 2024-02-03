using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallationInventoryController : MonoBehaviour
{
    public InstallationController controller;
    public InstallationDestinationController destinationController;
    public AbstractInventory inventory;
    
    private string recipeIndex;
    
    private float spawnTimer = 0;
    private void Update()
    {
        if(controller.doughContainer.Count == 0 || !destinationController.destination[1])
            return;
        
        if(controller._installationData.haveIngredientInventory && controller.ingredients.Count == 0)
            return;

        spawnTimer += Time.deltaTime;
        
        if (spawnTimer > controller._installationData.makeDelay)
        {
            spawnTimer = 0;
            DoughSetController();
        }
    }

    public void DoughSetController()
    {

        GameObject curObj = controller.doughContainer.Dequeue();
        curObj.SetActive(true);
        
        IngredientController curController = curObj.GetComponent<IngredientController>();
        curController.destination = destinationController.destination[1];
        curController.InitSet();
        
        GameManager.instance.spawnManager.SpawnPositionSet(transform.root.gameObject, curController.destination, curObj);
        GameManager.instance.inventoryManager.RemoveItemFromInventory(inventory.inventoryID, curController.itemData, 1);

        if (controller._installationData.haveIngredientInventory)
        {
            ItemSO ingredientData = controller.ingredients.Dequeue();
            curController.addImageController.AddImage(ingredientData.sprite);
            GameManager.instance.inventoryManager.RemoveItemFromInventory(inventory.inventoryID, ingredientData, 1);
            curController.VisitIngredientDataSet(controller, ingredientData);
        }
        else if(!controller._installationData.haveIngredientInventory)
        {
            curController.VisitInstallationSet(controller);
        }
        
        
        if (controller._installationData.completeMake)
        {
            GameManager.instance.poolManager.DeSpawnFromPool(curObj);

            for (int i = curController.interactInstallation.Count - 1; i >= 0; i--)
            {
                recipeIndex += curController.interactInstallation.Dequeue() + "+";
            }
            
            int spawnFoodID = GameManager.instance.recipeManager.CompareWithResipe(recipeIndex);
            GameManager.instance.spawnManager.SpawnIngredient(gameObject, destinationController.destination[1], GameManager.instance.dataManager.foodSub[spawnFoodID]);

            recipeIndex = "";
        }
    }
}
