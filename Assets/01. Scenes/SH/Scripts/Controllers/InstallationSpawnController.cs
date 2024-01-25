using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallationSpawnController : MonoBehaviour
{
    public InstallationController _installationController;
    public float spawnTimer;
    
    private void Update()
    {
        if(!_installationController.destinationObj || !_installationController._installationData.canSpawn)
            return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer > _installationController._installationData.spawnDelay)
        {
            spawnTimer = 0f;
                SpawnManager.instance.SpawnIngredient(gameObject, _installationController, _installationController._installationData.spawnData);
        }
    }
}
