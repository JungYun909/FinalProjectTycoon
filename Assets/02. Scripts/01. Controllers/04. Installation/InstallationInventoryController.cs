using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallationInventoryController : MonoBehaviour
{
    public InstallationController controller;
    public InstallationDestinationController destinationController;
    public AbstractInventory inventory;
    
    private int recipeIndex;
    
    private float spawnTimer = 0;
    private void Update()
    {
        if(controller.doughContainer.Count == 0 || !destinationController.destination[1])
            return;

        spawnTimer += Time.deltaTime;
        
        if (spawnTimer > controller._installationData.spawnDelay && !controller._installationData.completeMake)
        {
            spawnTimer = 0;
            DoughSetController();
        }

        if (spawnTimer > controller._installationData.makeDelay && controller._installationData.completeMake)
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
        
        if (controller._installationData.completeMake)
        {
            GameManager.instance.poolManager.DeSpawnFromPool(curObj);

            for (int i = curController.interactInstallation.Count - 1; i >= 0; i--)
            {
                recipeIndex += (int)Math.Pow(10, i) * curController.interactInstallation.Dequeue();
            }
            
            Debug.Log("recipeIndex" + recipeIndex);
            
            int spawnFoodID = GameManager.instance.recipeManager.CompareWithResipe(recipeIndex);
            Debug.Log("spawnFoodID" + spawnFoodID);
            GameManager.instance.spawnManager.SpawnIngredient(gameObject, destinationController.destination[1], GameManager.instance.dataManager.foodSub[spawnFoodID-1]);

            recipeIndex = 0;
        }
    }
}
