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
    
    public GameObject destinationObj;
    public Queue<GameObject> doughContainer;
    
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
            InstallationController controller = InstallationManager.instance.curInstallation.GetComponent<InstallationController>();
            controller.destinationObj = gameObject;
            InstallationManager.instance.OnInstallationSetUI();
        }
    }

    public void OnColliderInteract()
    {
        //온콜라이더 상호작용 내용
    }
}
