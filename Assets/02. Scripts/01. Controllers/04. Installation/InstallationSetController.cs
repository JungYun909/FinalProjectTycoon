using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
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
        GameManager.instance.poolManager.DeSpawnFromPool(curGameObject);
        GameManager.instance.installationManager.installationManageController.SetActive(false);
        GameManager.instance.dataManager.SaveInstallation(curGameObject);
        GameManager.instance.dataManager.SaveData();
        curGameObject = null;
    }

    public void InstallationInstall()
    {
        RaycastHit2D[] ray = Physics2D.RaycastAll(gameObject.transform.position, Vector2.zero, 0f);
        Debug.Log(ray.Length);
        if(ray.Length > 2 || ray.Length < 2)
            return;
        
        GameManager.instance.installationManager.installationManageController.SetActive(false);
        GameManager.instance.dataManager.PosUpdate(curGameObject);
        GameManager.instance.dataManager.SaveData();
        curGameObject = null;
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
