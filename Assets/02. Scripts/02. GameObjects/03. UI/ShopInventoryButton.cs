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

        Debug.Log("[ShopInventoryButton] OnEnable - Event subscription added");
    }


    private void SetButtonToSpawnInstallation()
    {
        button.onClick.AddListener(SpawnInstallation);
    }

    private void SetButtonToShowQuantityController()
    {
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
        if (shopUI.curMachine.price > GameManager.instance.dataManager.playerData.money)
        {
            //TODO 경고창
            return;
        }
        GameManager.instance.statManager.SpendGold(shopUI.curMachine.price);

        GameObject obj = GameManager.instance.spawnManager.SpawnInstallaion(shopUI.curMachine);
        GameManager.instance.uiManager.CloseAll();
    }

    private void ShowQuantityController()
    {
        Debug.Log("[ShopInventoryButton] ShowQuantityController called");
        shopUI.quantityCheck.SetActive(true);
        quantityController.DeliverQuantity += HandlePurchaseItem;
    }

    private void HandlePurchaseItem(int quantity)
    {
        Debug.Log("Purchase Occured");
        if(GameManager.instance.dataManager.playerData.money < shopUI.curItem.price * quantity)
        {
            return;
        }
        else
        {
            GameManager.instance.statManager.SpendGold(shopUI.curItem.price * quantity);
            GameManager.instance.inventoryManager.AddItemToInventory(1000, shopUI.curItem, quantity);
        }
        quantityController.DeliverQuantity -= HandlePurchaseItem;
    }
}
