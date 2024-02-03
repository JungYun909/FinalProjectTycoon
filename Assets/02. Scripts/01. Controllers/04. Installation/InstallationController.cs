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

    public GameObject spawnFunction;
    public GameObject inventoryFunction;
    public GameObject moveFunction;
    public GameObject destinationFunction;

    private RuntimeAnimatorController animatorController;

    public AbstractInventory inventoryController;

    public Queue<GameObject> doughContainer;
    public Queue<ItemSO> ingredients;

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
        animatorController = _animData.installtionAnimController[_installationData.id-1];
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = _installationData.sprite;
        //deliverInventoryInfo?.Invoke(_installationData);
        gameObject.GetComponentInChildren<Animator>().runtimeAnimatorController = animatorController;

        if (_installationData.haveDoughInventory)
        {
            doughContainer = new Queue<GameObject>();
            ingredients = new Queue<ItemSO>();
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
