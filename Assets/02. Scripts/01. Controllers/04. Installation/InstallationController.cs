using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InstallationController : MonoBehaviour
{
    public MachineSO _installationData;
    public GameObject spawnFunction;
    public GameObject inventoryFunction;

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
}
