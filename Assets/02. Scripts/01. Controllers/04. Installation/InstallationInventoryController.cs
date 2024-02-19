using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallationInventoryController : MonoBehaviour
{
    public InstallationController controller;
    public InstallationDestinationController destinationController;
    public AbstractInventory inventory;

    private float eventTimer = 0f;
    private const float eventInterval = 1f;

    public event Action<float> deliverCurrentTime;
    
    private string recipeIndex;
    
    private float spawnTimer = 0;
    private void Update()
    {
        if(controller.doughContainer.Count == 0 || !destinationController.destination[1])
            return;
        
        if(controller._installationData.haveIngredientInventory && controller.ingredients.Count <= 0)
            return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer > controller._installationData.makeDelay)
        {
            spawnTimer = 0;
            DoughSetController();
        }
        eventTimer += Time.deltaTime;
        if (eventTimer >= eventInterval)
        {
            deliverCurrentTime?.Invoke(spawnTimer);
            eventTimer = 0;
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
            Debug.Log(ingredientData.id);
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
            
            recipeIndex = "";
            GameManager.instance.spawnManager.SpawnIngredient(gameObject, destinationController.destination[1], GameManager.instance.dataManager.foodSub[spawnFoodID]);

        }
    }
}
