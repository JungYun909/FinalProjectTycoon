using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallationSpawnController : MonoBehaviour
{
    public InstallationDestinationController _destiantionController;
    public InstallationController _controller;
    public float spawnTimer;
    
    private void Update()
    {
        if(!_destiantionController.destination[1] || !_controller._installationData.canSpawn)
            return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer > _controller._installationData.spawnDelay)
        {
            spawnTimer = 0f;
                SpawnManager.instance.SpawnIngredient(gameObject, _destiantionController.destination[1], _controller._installationData.spawnData);
        }
    }
}
