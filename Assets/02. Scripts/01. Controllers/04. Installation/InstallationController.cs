using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InstallationController : MonoBehaviour, IInteractable
{
    public MachineSO _installationData;

    public AbstractInventory inventory;
    public InstallationInventoryController inventoryController;
    public InstallationInstallController installController;
    public InstallationDestinationController destinationController;
    public InstallationSpawnController spawnController;
    public InstallationAnimationController animController;

    public Queue<GameObject> doughContainer = new Queue<GameObject>();
    public Queue<ItemSO> ingredients = new Queue<ItemSO>();

    private int index = 0;

    public event Action installationFuctionSet;
    public event Action installationFuctionOut;


    public int destinationID;

    private void Start()
    {
        InitSetting();
    }

    private void OnEnable()
    {
        StartCoroutine(DelayLoadingDestinationInfo());
    }
    private IEnumerator DelayLoadingDestinationInfo()
    {
        yield return new WaitForSeconds(1f);
        LoadDestinationDataAndSetDestination();
    }    
    private void LoadDestinationDataAndSetDestination()
    {
        destinationController.gameObject.SetActive(true);
        List<DestinationData> allDestinationInfo = GameManager.instance.destinationManager.destinationInfo;
        DestinationData loadedData = allDestinationInfo.Find(data => data.controllerID == this.destinationID);
        if (loadedData != null)
        {
            if (loadedData.controllerID == this.destinationID)
            {
                InstallationController connectedController = GameManager.instance.destinationManager.GetDestinationGameObject(loadedData.connectedControllerID);
                if (connectedController != null)
                {
                    InstallationDestinationController destinationController = GetComponentInChildren<InstallationDestinationController>();
                    destinationController.destination[0] = this.gameObject;
                    destinationController.destination[1] = connectedController.gameObject;
                    destinationController.desPos1 = connectedController.gameObject.transform.position;
                }
            }
            else
            {
                return;
            }
        }
    }

    public void InitSetting()
    {
        if(_installationData == null)
            return;
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = _installationData.sprite;

        if (_installationData.haveDoughInventory)
        {
            inventoryController.gameObject.SetActive(true);
            inventoryController.InitSet();
        }

        if (_installationData.canSpawn)
        {
            spawnController.gameObject.SetActive(true);
            spawnController.InitSet();
        }
        
        if (_installationData.animation.Count > 0 && _installationData.haveDoughInventory || _installationData.canSpawn)
        {
            animController.AddAnimation(_installationData.animation[(int)InstallationAnimType.Spawn], InstallationAnimType.Spawn);
        }
    }

    public void InitializeDestinationSetting(int destinationID)
    {
        this.destinationID = destinationID;
        GameManager.instance.destinationManager.RegisterDestinationID(this);
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
                    inventory.InitSet();
                    installController.gameObject.SetActive(false);
                    destinationController.gameObject.SetActive(false);
                    break;
                case 1:
                    installController.gameObject.SetActive(true);
                    installController.InitSet();
                    destinationController.gameObject.SetActive(false);
                    break;
                case 2:
                    installController.gameObject.SetActive(false);
                    destinationController.gameObject.SetActive(true);
                    destinationController.InitSet();
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
