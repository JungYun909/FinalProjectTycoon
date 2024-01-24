using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private Inventory _inventory;
    private InstallationController _installationController;

    private float makeTimer;
    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
        _installationController = GetComponent<InstallationController>();
    }

    private void Start()
    {
        InstallationManager.instance.curInstallation = gameObject;
        InstallationManager.instance.OnInstallationSetUI();
        _inventory.StartSet();
    }

    // Update is called once per frame
    private void Update()
    {
        if(!_installationController._installationData.haveDoughInventory)
            return;

        makeTimer += Time.deltaTime;

        if (makeTimer > _installationController._installationData.makeDelay)
        {
            makeTimer = 0f;
            
            if(_installationController._installationData.haveDoughInventory)
                _inventory.RemoveDough();
            
            if(_installationController._installationData.haveIngredientInventory)
                _inventory.RemoveIngredient();
        }
    }
}
