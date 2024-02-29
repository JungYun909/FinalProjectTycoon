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
    public GameObject errorMessage;

    private void OnEnable()
    {
        shopUI = GetComponentInParent<ShopUI>();
        shopUI.onIngredientEnabled += SetButtonToShowQuantityController;
        shopUI.onMachineEnabled += SetButtonToSpawnInstallation;
        shopUI.onToolEnabled += SetButtonToShowQuantityController;
        SetButtonToSpawnInstallation();
    }


    private void SetButtonToSpawnInstallation()
    {
        button.onClick.RemoveAllListeners(); // 이전 리스너 제거
        button.onClick.AddListener(SpawnInstallation);
    }

    private void SetButtonToShowQuantityController()
    {
        button.onClick.RemoveAllListeners(); // 이전 리스너 제거
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
            errorMessage.SetActive(true);
            return;
        }
        GameManager.instance.statManager.SpendGold(shopUI.curMachine.price);
        GameObject obj = GameManager.instance.spawnManager.SpawnInstallaion(shopUI.curMachine);
        GameManager.instance.uiManager.CloseAll();
    }

    private void ShowQuantityController()
    {
        shopUI.quantityCheck.SetActive(true);
        quantityController.DeliverQuantity += HandlePurchaseItem;
    }

    private void HandlePurchaseItem(int quantity)
    {
        if (GameManager.instance.dataManager.playerData.money < shopUI.curItem.price * 2 * quantity)
        {
            errorMessage.SetActive(true);
            return;
        }
        else if (quantity == 0)
            return;
        else
        {
            GameManager.instance.statManager.SpendGold(shopUI.curItem.price * 2 * quantity);
            GameManager.instance.inventoryManager.AddItemToInventory(1000, shopUI.curItem, quantity);
        }
        quantityController.DeliverQuantity -= HandlePurchaseItem;
    }
}
