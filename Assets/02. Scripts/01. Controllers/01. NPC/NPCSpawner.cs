using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class NPCSpawner : MonoBehaviour
{
    private float spawnTimer;
    private float spawnTime = 1f;
    private int spawnPercentage = 100;

    private NPCSeparation _separation;
    private WaitForSeconds waitSecond = new WaitForSeconds(1f);

    private void OnEnable()
    {
        _separation = GetComponent<NPCSeparation>();
        StartCoroutine(SpawnNPCCoroutine());
    }

    private void OnDisable()
    {
        GameManager.instance.spawnManager.curNpcCount = 0;
        StopCoroutine(SpawnNPCCoroutine());
    }

    private IEnumerator SpawnNPCCoroutine()
    {
        while (true)
        {
            yield return waitSecond;
            if (GameManager.instance.spawnManager.curNpcCount > GameManager.instance.statManager.maxNpc ||
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != SceneType.MainScene.ToString())
            {
                continue;
            }
            int random = Random.Range(1, 101);
            if (random < spawnPercentage * (GameManager.instance.dataManager.playerData.level * 0.15f))
            {
                NpcSO curNPC = _separation.NPCChoice(GameManager.instance.dataManager.playerData.level);
                GameManager.instance.spawnManager.SpawnNPC(curNPC);
            }
        }
    }
}
