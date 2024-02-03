using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopInventoryButton : MonoBehaviour
{
    public Button button;
    public QuantityController quantityController;
    ShopUI shopUI;

    private void OnEnable()
    {
        shopUI = GetComponentInParent<ShopUI>();
        shopUI.onIngredientEnabled += SetButtonToShowQuantityController;
        shopUI.onMachineEnabled += SetButtonToSpawnInstallation;
        Debug.Log("Event Subscribed");
    }

    private void SetButtonToSpawnInstallation()
    {
        Debug.Log("Now Spawns Installation");
        button.onClick.AddListener(SpawnInstallation);
    }

    private void SetButtonToShowQuantityController()
    {
        Debug.Log("Now Enables Quantity controller");
        button.onClick.AddListener(ShowQuantityController);
    }
    private void OnDisable()
    {
        shopUI.onIngredientEnabled -= ShowQuantityController;
        shopUI.onMachineEnabled -= SpawnInstallation;

        button.onClick.RemoveListener(ShowQuantityController);
        button.onClick.RemoveListener(SpawnInstallation);
    }
    private void SpawnInstallation()
    {
        if(shopUI.machineSO.price > GameManager.instance.statManager.currentGold)
        {
            //TODO 경고창
            return;
        }
        GameManager.instance.statManager.SpendGold(shopUI.machineSO.price);

        GameObject obj = GameManager.instance.spawnManager.SpawnInstallaion(shopUI.machineSO);
        GameManager.instance.uiManager.CloseAll();
    }

    private void ShowQuantityController()
    {
        shopUI.quantityCheck.SetActive(true);
        quantityController.DeliverQuantity += HandlePurchaseItem;

    }

    private void HandlePurchaseItem(int quantity)
    {
        Debug.Log("Purchase Occured");
        if(GameManager.instance.statManager.currentGold < shopUI.curItem.price * quantity)
        {
            return;
        }
        else
        {
            GameManager.instance.statManager.SpendGold(shopUI.curItem.price * quantity);
            GameManager.instance.inventoryManager.AddItemToInventory(shopUI.shopInventoryID, shopUI.curItem, quantity);
        }
        quantityController.DeliverQuantity -= HandlePurchaseItem;
    }
}
