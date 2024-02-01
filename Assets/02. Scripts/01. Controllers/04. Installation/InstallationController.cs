using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InstallationController : MonoBehaviour, IInteractable
{
    public MachineSO _installationData;
    public GameObject spawnFunction;
    public GameObject inventoryFunction;
    public GameObject moveFunction;
    public GameObject destinationFunction;

    public AbstractInventory inventoryController;

    public Queue<GameObject> doughContainer;

    private int index = 0;

    public event Action installationFuctionSet;
    public event Action installationFuctionOut;
    //public event Action<AbstractInventory> deliverInventoryInfo;

    private void Start()
    {
        InitSetting();
    }
    public void InitSetting()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = _installationData.sprite;
        //deliverInventoryInfo?.Invoke(_installationData);
        
        
        if (_installationData.haveDoughInventory)
        {
            doughContainer = new Queue<GameObject>();
            inventoryFunction.SetActive(true);
        }
            
        if(_installationData.canSpawn)
            spawnFunction.SetActive(true);
    }
    

    public bool Continuous()
    {
        return false;
    }

    public void OnClickInteract()
    {
        if (index != GameManager.instance.interactionManager.installationFunctionIndex)
        {
            installationFuctionSet = null;
            
            switch (GameManager.instance.interactionManager.installationFunctionIndex)
            {
                case 0:    
                    inventoryController.InitSet();
                    moveFunction.SetActive(false);
                    destinationFunction.SetActive(false);
                    break;
                case 1:
                    moveFunction.SetActive(true);
                    destinationFunction.SetActive(false);
                    break;
                case 2:
                    moveFunction.SetActive(false);
                    destinationFunction.SetActive(true);
                    break;
            }

            index = GameManager.instance.interactionManager.installationFunctionIndex;
        }
        installationFuctionSet?.Invoke();
    }

    public void OffClickInteract()
    {
        installationFuctionOut?.Invoke();
    }

    public void OnColliderInteract()
    {
        return;
    }
}
