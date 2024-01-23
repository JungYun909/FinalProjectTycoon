using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallationController : MonoBehaviour
{
    public InstallationData _installationData;
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
                GameObject curSpawnObject = PoolManager.instacne.SpawnFromPool(_installationData.stat.spawnPrefab);
                curSpawnObject.transform.position = gameObject.transform.position;

                if (curSpawnObject.GetComponent<MovementController>())
                {
                    curSpawnObject.GetComponent<MovementController>().Move(_installationData.stat.destinationInstallation, _installationData.stat.moveSpeed);
                }
            }
        }
    }
}
