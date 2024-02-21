using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InstallationInventoryController : MonoBehaviour
{
    public InstallationController _controller;
    public InstallationDestinationController destinationController;
    public AbstractInventory inventory;

    public float eventTimer = 0f;
    private const float eventInterval = 1f;

    public event Action<float> deliverCurrentTime;
    
    private string recipeIndex;
    
    public float spawnTimer = 0;
    
    private float animTime;
    private bool startAnim;

    public void InitSet()
    {
        animTime = _controller._installationData.animation[(int)InstallationAnimType.Spawn].length;
    }
    private void Update()
    {
        if(_controller.doughContainer.Count == 0 || !destinationController.destination[1])
            return;
        
        if(_controller._installationData.haveIngredientInventory && _controller.ingredients.Count <= 0)
            return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer > _controller._installationData.makeDelay - (animTime * 0.8f) && startAnim == false)
        {
            _controller.animController.StartSpawnAnim();
            startAnim = true;
        }

        if (spawnTimer > _controller._installationData.makeDelay)
        {
            spawnTimer = 0;
            startAnim = false;
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
        GameObject curObj = _controller.doughContainer.Dequeue();
        curObj.SetActive(true);
        
        IngredientController curController = curObj.GetComponent<IngredientController>();
        curController.destination = destinationController.destination[1];
        curController.InitSet();
        
        GameManager.instance.spawnManager.SpawnPositionSet(transform.root.gameObject, curController.destination, curObj);
        GameManager.instance.inventoryManager.RemoveItemFromInventory(inventory.inventoryID, curController.itemData, 1);

        if (_controller._installationData.haveIngredientInventory)
        {
            ItemSO ingredientData = _controller.ingredients.Dequeue();
            Debug.Log(ingredientData.id);
            GameManager.instance.inventoryManager.RemoveItemFromInventory(inventory.inventoryID, ingredientData, 1);
            curController.VisitIngredientDataSet(_controller, ingredientData);
        }
        else if(!_controller._installationData.haveIngredientInventory)
        {
            curController.VisitInstallationSet(_controller);
        }
        
        
        if (_controller._installationData.completeMake)
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
