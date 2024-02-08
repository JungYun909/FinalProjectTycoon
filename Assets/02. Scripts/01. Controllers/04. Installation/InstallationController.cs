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

    private void Start()
    {
        InitSetting();
    }
    public void InitSetting()
    {
        animatorController = _animData.installtionAnimController[_installationData.id-1];
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = _installationData.sprite;
        gameObject.GetComponentInChildren<Animator>().runtimeAnimatorController = animatorController;

        if (_installationData.haveDoughInventory)
        {
           doughContainer = new Queue<GameObject>();
            ingredients = new Queue<ItemSO>();
            inventoryController.gameObject.SetActive(true);
        }
            
        if(_installationData.canSpawn)
            spawnController.gameObject.SetActive(true);
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
