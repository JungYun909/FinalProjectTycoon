using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class NPCSpawner : MonoBehaviour
{
    private float spawnTimer;
    private float spawnTime = 2f;
    private int spawnPercentage = 100;

    private void Update()
    {
        if (GameManager.instance.spawnManager.curNpcCount > GameManager.instance.statManager.maxNpc || UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != SceneType.MainScene.ToString())
            return;
        
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnTime)
        {
            spawnTimer = 0f;

            int random = Random.Range(1, 101);

            if (random < spawnPercentage * (GameManager.instance.statManager.shopLevel * 0.2f))
            {
                GameManager.instance.spawnManager.SpawnNPC();
            }
        }
    }
}
