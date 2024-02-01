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
        if(!_destiantionController.destination[1] || !_controller._installationData.canSpawn || GameManager.instance.statManager.currentGold < _controller._installationData.spawnData.price)
            return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer > _controller._installationData.spawnDelay)
        {
            spawnTimer = 0f;
            GameManager.instance.statManager.SpendGold(_controller._installationData.spawnData.price);
            GameManager.instance.spawnManager.SpawnIngredient(gameObject, _destiantionController.destination[1], _controller._installationData.spawnData);
        }
    }
}
