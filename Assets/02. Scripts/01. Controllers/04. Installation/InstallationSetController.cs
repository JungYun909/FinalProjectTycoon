using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InstallationSetController : UIBase
{
    public GameObject curGameObject;
    [SerializeField] private InstallationMoveController _moveController;

    private void Start()
    {
        InitSet();
    }

    public void InitSet()
    {
        gameObject.SetActive(false);
        GameManager.instance.installationManager.installationManageController = gameObject;
        _moveController.tilemap = GameObject.Find("Grid").GetComponentInChildren<Tilemap>();
    }

    public void InstallationDestroy()
    {
        InstallationController curController = curGameObject.GetComponent<InstallationController>();
        if (!curController._installationData.canSpawn)
        {
            AbstractInventory curInventory = curGameObject.GetComponentInChildren<AbstractInventory>();
            Debug.Log($"inventory has {curInventory.Items.Count} items");
            if(curInventory.Items.Count>0)
            {
                Debug.Log("Opening Error Window");
                GameManager.instance.uiManager.OpenErrorWindow();
                return;
            }
            int curInventoryID = curInventory.inventoryID;
            GameManager.instance.inventoryManager.DeleteInventoryData(curInventoryID);
        }
        GameManager.instance.inventoryManager.AddMachineToPlayerInventory(curController._installationData, 1);
        GameManager.instance.destinationManager.DeleteDestinationInfo(curController.destinationID);
        GameManager.instance.poolManager.DeSpawnFromPool(curGameObject);
        GameManager.instance.installationManager.installationManageController.SetActive(false);
        GameManager.instance.dataManager.RemoveInstallationData(curGameObject);
        InstallationEndSet();
    }

    public void InstallationInstall()
    {
        RaycastHit2D[] rays = Physics2D.RaycastAll(gameObject.transform.position, Vector2.zero, 0f);
        
        if(rays.Length > 2 || rays.Length < 2)
            return;

        InstallationController controller = curGameObject.GetComponent<InstallationController>();

        foreach (var ray in rays)
        {
            if (controller._installationData.installasionName != "진열대")
            {
                if (ray.collider.tag == "Store")
                {
                    return;
                }
            }
            else if(controller._installationData.installasionName == "진열대")
            {
                if (ray.collider.tag == "Kitchen")
                {
                    return;
                }
            }
        }
        
        GameManager.instance.installationManager.installationManageController.SetActive(false);
        GameManager.instance.dataManager.PosUpdate(curGameObject);
        InstallationEndSet();
    }

    private void InstallationEndSet()
    {
        curGameObject = null;
        GameManager.instance.interactionManager.interactionObject = null;
    }

    public override void Initialize()
    {
        return;
    }

    public override void UpdateUI()
    {
        return;
    }
}
