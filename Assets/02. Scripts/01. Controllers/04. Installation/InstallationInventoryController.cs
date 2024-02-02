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

        if (controller._installationData.haveIngredientInventory)
        {
            //TODO 여기에 해당 기능 스크립트의 메소드 불러주기 (설치물 컨트롤러에 있는 재료 정보 큐에서 디큐해서 스프라이트 받아서 바꿔끼워주기)
        }
        
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
                recipeIndex += curController.interactInstallation.Dequeue() + "+";
            }
            
            Debug.Log("recipeIndex" + recipeIndex);
            
            int spawnFoodID = GameManager.instance.recipeManager.CompareWithResipe(recipeIndex);
            GameManager.instance.spawnManager.SpawnIngredient(gameObject, destinationController.destination[1], GameManager.instance.dataManager.foodSub[spawnFoodID-1]);

            recipeIndex = "";
        }
    }
}
