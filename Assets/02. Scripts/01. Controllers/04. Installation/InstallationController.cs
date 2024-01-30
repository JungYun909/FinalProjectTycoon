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

    public Queue<GameObject> doughContainer;

    private int index = -1;

    public event Action installationFuctionSet;
    public event Action installationFuctionOut;
    
    private void Start()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = _installationData.sprite;
        
        if (_installationData.haveDoughInventory)
            doughContainer = new Queue<GameObject>();

        if(_installationData.canSpawn)
            spawnFunction.SetActive(true);
        
        if(_installationData.haveDoughInventory)
            inventoryFunction.SetActive(true);
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
                    inventoryFunction.SetActive(true);
                    moveFunction.SetActive(false);
                    destinationFunction.SetActive(false);
                    break;
                case 1:
                    inventoryFunction.SetActive(false);
                    moveFunction.SetActive(true);
                    destinationFunction.SetActive(false);
                    break;
                case 2:
                    inventoryFunction.SetActive(false);
                    moveFunction.SetActive(false);
                    destinationFunction.SetActive(true);
                    break;
            }
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
