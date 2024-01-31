using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnController : MonoBehaviour
{
    [SerializeField] private int reputation;
    [SerializeField] private float time;
    [SerializeField] private int NPCNum;
    [SerializeField] private GameObject npc;
    [SerializeField] private int maxNpc;
    private Coroutine coroutine;
    [SerializeField] GameObject positionNum;


    // Start is called before the first frame update
    void Start()
    {

        maxNpc = 1;
        //maxNpc = GameManager.instance.statManager.maxNpc;
        coroutine = StartCoroutine(NPCSpawnCorutine());


    }

    private void Update()
    {
        if (NPCNum > maxNpc)
        {
            StopCoroutine(coroutine);
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
        Debug.Log(rand);

        if (rand <= visitProbability)
        {
            NPCNum += 1;
            GameObject curNPC =  GameManager.instance.poolManager.SpawnFromPool(npc);
            curNPC.transform.position = positionNum.transform.position;
            
            npc.SetActive(true);
            Debug.Log("손님이 왕이다");
            

        }
    }

  
}
