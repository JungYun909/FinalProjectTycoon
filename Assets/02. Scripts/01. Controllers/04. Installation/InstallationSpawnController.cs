using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallationSpawnController : MonoBehaviour
{
    public InstallationDestinationController _destiantionController;
    public InstallationController _controller;
    public float spawnTimer;

    private bool startAnim;
    private float animTime;
    public event Action OnSpawnEvent;

    public void InitSet()
    {
        animTime = _controller._installationData.animation[(int)InstallationAnimType.Spawn].length;
    }

    private void Update()
    {
        if(!_destiantionController.destination[1] || !_controller._installationData.canSpawn || GameManager.instance.dataManager.playerData.money < _controller._installationData.spawnData.price)
            return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer > _controller._installationData.spawnDelay - (animTime * 0.8f) && startAnim == false)
        {
            _controller.animController.StartSpawnAnim();
            startAnim = true;
        }

        if (spawnTimer > _controller._installationData.spawnDelay)
        {
            OnSpawnEvent?.Invoke();
            
            spawnTimer = 0f;
            startAnim = false;
            GameManager.instance.statManager.SpendGold(_controller._installationData.spawnData.price);
            GameManager.instance.spawnManager.SpawnIngredient(gameObject, _destiantionController.destination[1], _controller._installationData.spawnData);
        }
    }
}
