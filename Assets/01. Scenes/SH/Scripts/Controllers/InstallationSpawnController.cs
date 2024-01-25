using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InstallationSpawnController : MonoBehaviour
{
    public InstallationController _installationController;
    public float spawnTimer;

    private void Start()
    {
        _installationController = GetComponent<InstallationController>();
    }

    private void Update()
    {
        if(!_installationController._installationData.destinationInstallation || !_installationController._installationData.canSpawn)
            return;

        spawnTimer += Time.deltaTime;
        
        _installationController._installationData.spawnData.destination =
            _installationController._installationData.destinationInstallation;

        if (spawnTimer > _installationController._installationData.spawnDelay)
        {
            spawnTimer = 0f;
                SpawnManager.instance.SpawnIngredient(gameObject, _installationController._installationData.spawnData);
        }
    }
}
