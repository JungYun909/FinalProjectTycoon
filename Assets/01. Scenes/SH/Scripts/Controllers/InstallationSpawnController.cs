using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InstallationSpawnController : MonoBehaviour
{
    public InstallationController _installationController;
    public float spawnTimer;

    private void Awake()
    {
        _installationController = GetComponent<InstallationController>();
    }

    private void Update()
    {
        if(!_installationController._installationData.destinationInstallation || !_installationController._installationData.canSpawn)
            return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer > _installationController._installationData.spawnDelay)
        {
            spawnTimer = 0f;
                SpawnManager.instance.Spawn(gameObject, _installationController._installationData.name);
        }
    }
}
