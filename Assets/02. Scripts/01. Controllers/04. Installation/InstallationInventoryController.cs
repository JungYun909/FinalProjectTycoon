using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallationInventoryController : MonoBehaviour
{
    public InstallationController controller;
    public InstallationDestinationController destinationController;
    public AbstractInventory inventory;
    
    private float spawnTimer = 0;
    private void Update()
    {
        if(controller.doughContainer.Count == 0 || !destinationController.destination[1])
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
        GameManager.instance.spawnManager.SpawnPositionSet(transform.root.gameObject, curController.destination, curObj);
        GameManager.instance.inventoryManager.RemoveItemFromInventory(inventory.inventoryID, curController.itemData, 1);
    }
}
