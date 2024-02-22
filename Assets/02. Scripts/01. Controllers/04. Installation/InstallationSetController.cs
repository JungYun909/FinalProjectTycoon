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
        GameManager.instance.inventoryManager.AddMachineToPlayerInventory(curGameObject.GetComponent<InstallationController>()._installationData, 1);
        GameManager.instance.poolManager.DeSpawnFromPool(curGameObject);
        GameManager.instance.installationManager.installationManageController.SetActive(false);
        GameManager.instance.dataManager.SaveInstallation(curGameObject);
        InstallationEndSet();
    }

    public void InstallationInstall()
    {
        RaycastHit2D[] rays = Physics2D.RaycastAll(gameObject.transform.position, Vector2.zero, 0f);
        Debug.Log(rays.Length);
        if(rays.Length > 2 || rays.Length < 2)
            return;

        InstallationController controller = curGameObject.GetComponent<InstallationController>();

        foreach (var ray in rays)
        {
            if (controller._installationData.installasionName != "진열대")
            {
                if (ray.collider.tag == "Store")
                {
                    Debug.Log("Warning Can't install here");
                    return;
                }
            }
            else if(controller._installationData.installasionName == "진열대")
            {
                if (ray.collider.tag == "Kitchen")
                {
                    Debug.Log("Warning Can't install here");
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
        GameManager.instance.dataManager.SaveData();
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
