using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InstallationController : MonoBehaviour, IInteractable
{
    public InstallationData _installationData;
    public GameObject spawnFunction;
    public GameObject inventoryFunction;
    
    private void Start()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = _installationData.sprite;
        
        if (_installationData.haveDoughInventory)
            _installationData.doughContainer = new Queue<GameObject>();

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
        GameObject curInstallationSetObj = InstallationManager.instance.curInstallation;

        if (!curInstallationSetObj)
        {
            InstallationManager.instance.curInstallation = gameObject;
            InstallationManager.instance.OnInstallationSetUI();
        }
        else if (curInstallationSetObj == gameObject)
        {
            InstallationManager.instance.OnInstallationSetUI();
        }
        else
        {
            InstallationManager.instance.curInstallation.GetComponent<InstallationController>()._installationData.destinationInstallation =
                gameObject; //관리중인 오브젝트의 목표지로 설정
            InstallationManager.instance.OnInstallationSetUI();
        }
    }

    public void OnColliderInteract()
    {
        //온콜라이더 상호작용 내용
    }
}
