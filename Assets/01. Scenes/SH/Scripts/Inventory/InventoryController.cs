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
        //목적지가 있어야 소환함
        if(!_installationController._installationData.destinationInstallation)
            return;
        
        //인벤토리 상황에 따라 소환함
        if (_installationController._installationData.haveDoughInventory && !_installationController._installationData.haveIngredientInventory && _inventory.doughSlots[0].quantity > 0)
            makeTimer += Time.deltaTime;
        else if (_installationController._installationData.haveDoughInventory && _installationController._installationData.haveIngredientInventory && _inventory.doughSlots[0].quantity > 0 &&
                 _inventory.ingredientSlots[0].quantity > 0)
            makeTimer += Time.deltaTime;
        else
        {
            makeTimer = 0;
            return;
        }
            

        if (makeTimer > _installationController._installationData.makeDelay)
        {
            makeTimer = 0f;

            InventoryUpdate();

            SpawnObjSetUpdate();
        }
    }


    private void InventoryUpdate()
    {
        if(_installationController._installationData.haveDoughInventory)
            _inventory.RemoveDough();
            
        if(_installationController._installationData.haveIngredientInventory)
            _inventory.RemoveIngredient();
    }
    private void SpawnObjSetUpdate()
    {
        GameObject curDoughObj = _installationController._installationData.doughContainer.Dequeue();
        SpawnManager.instance.SpawnObjPositionSet(gameObject, curDoughObj, _installationController._installationData.spawnData);
        curDoughObj.SetActive(true);
        curDoughObj.GetComponentInChildren<SpriteRenderer>().color = new Color(0.6f, 0.4f, 0.2f);
    }
}
