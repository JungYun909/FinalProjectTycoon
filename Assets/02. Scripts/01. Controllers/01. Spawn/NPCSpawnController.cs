using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnController : MonoBehaviour
{
    [SerializeField] private int reputation;
    [SerializeField] private float time;
    [SerializeField] private int NPCNum;
    private PoolManager poolManager;
    [SerializeField] private GameObject npc;
    [SerializeField] GameObject positionNum;


    // Start is called before the first frame update
    void Start()
    {
        poolManager = GetComponent<PoolManager>();
        StartCoroutine(NPCSpawnCorutine());

    }


    void Update()
    {
        if (NPCNum==20)
        {
            StopCoroutine(NPCSpawnCorutine());
        }
    }

    IEnumerator NPCSpawnCorutine()
    {
        
        while(true)
        {
            NPCSpawn();
            yield return new WaitForSeconds(time);
        }
        

    }

    //기본 평판에 따른 리스폰 로직
    void NPCSpawn()
    {
        float visitProbability = reputation * 0.1f;
        int rand = UnityEngine.Random.Range(1, 100);

        if (rand <= visitProbability)
        {
            GameObject curNPC =  PoolManager.instacne.SpawnFromPool(npc);
            curNPC.transform.position = positionNum.transform.position;
            npc.SetActive(true);
            Debug.Log("손님이 왕이다");
            NPCNum += 1;

        }
    }

  
}
