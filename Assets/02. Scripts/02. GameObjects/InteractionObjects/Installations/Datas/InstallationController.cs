using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallationController : MonoBehaviour
{
    public InstallationData _installationData;
    public GameObject curSpawnObject;
    private float spawnTimer;

    private void Start()
    {
        _installationData.InitSetting();
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        
        if (spawnTimer > _installationData.stat.spawnDelay)
        {
            spawnTimer = 0;

            if (_installationData.stat.destinationInstallation)
            {
                
                //인벤토리가 없다면 설치물중 소환물이고 인벤토리가 있다면 제조물이다
                if (!_installationData.stat.haveInventory)
                {
                    curSpawnObject = PoolManager.instacne.SpawnFromPool(_installationData.stat.spawnPrefab);
                }
                else if(_installationData.stat.installationInventory.Count > 0)
                {
                    curSpawnObject = PoolManager.instacne.SpawnFromPool(_installationData.stat.installationInventory.Dequeue());
                }
                
                curSpawnObject.transform.position = gameObject.transform.position + ((_installationData.stat.destinationInstallation.transform.position - gameObject.transform.position).normalized);
                
                if (curSpawnObject.GetComponent<InstallationData>())
                {
                    curSpawnObject.GetComponent<IngredientData>().stat.VisitGameObjects.Add(gameObject);
                }

                if (curSpawnObject.GetComponent<MovementController>())
                {
                    curSpawnObject.GetComponent<MovementController>().Move(_installationData.stat.destinationInstallation);
                }
            }
        }
    }
}
