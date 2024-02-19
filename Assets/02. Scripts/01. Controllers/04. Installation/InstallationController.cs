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
    public MachineAnimSO _animData;

    private RuntimeAnimatorController animatorController;

    public AbstractInventory inventoryController;
    public InstallationInstallController installController;
    public InstallationDestinationController destinationController;
    public InstallationSpawnController spawnController;

    public Queue<GameObject> doughContainer;
    public Queue<ItemSO> ingredients;

    private int index = 0;

    public event Action installationFuctionSet;
    public event Action installationFuctionOut;


    public int destinationID;

    private void Start()
    {
        InitSetting();
        destinationID = GameManager.instance.destinationManager.RegisterInstallationDestinationController(this);
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
        DestinationData loadedData = GameManager.instance.dataManager.LoadDestinationData(this.destinationID) ;
        if (loadedData != null)
        {
            if (loadedData.controllerID == this.destinationID)
            {
                InstallationController connectedController = GameManager.instance.destinationManager.GetDestinationGameObject(loadedData.connectedControllerID);
                if (connectedController != null)
                {
                    InstallationDestinationController destinationController = GetComponentInChildren<InstallationDestinationController>();
                    destinationController.destination[1] = connectedController.gameObject;
                    destinationController.desPos1 = connectedController.gameObject.transform.position;
                }
            }
        }
    }


    public void SaveDestination()
    {
        if (this.GetComponentInChildren<InstallationDestinationController>().destination[1] != null)
        {
            int fromID = this.destinationID;
            int toID = this.GetComponentInChildren<InstallationDestinationController>().destination[1].GetComponent<InstallationController>().destinationID;
            DestinationData data = new DestinationData(fromID, toID);
            GameManager.instance.dataManager.SaveDestinationData(data);
        }
    }
    public void InitSetting()
    {
        if (_animData != null)
        {
            animatorController = _animData.installtionAnimController[_installationData.id - 1];
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = _installationData.sprite;
            gameObject.GetComponentInChildren<Animator>().runtimeAnimatorController = animatorController;

            if (_installationData.haveDoughInventory)
            {
                doughContainer = new Queue<GameObject>();
                ingredients = new Queue<ItemSO>();
                inventoryController.gameObject.SetActive(true);
            }

            if (_installationData.canSpawn)
                spawnController.gameObject.SetActive(true);
        }
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
