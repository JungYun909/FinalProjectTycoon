using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnController : MonoBehaviour
{
    [SerializeField] private int reputation;
    [SerializeField] private float time;
    [SerializeField] private int NPCNum;
    private PoolManager poolManager;

 
    // Start is called before the first frame update
    void Start()
    {
        poolManager = GetComponent<PoolManager>();
        //StartCoroutine(NPCSpawnCorutine());

    }


    void Update()
    {
        //if (NPCNum==20)
        //{
        //    StopCoroutine(NPCSpawnCorutine());
        //}
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
        //string npcNameTag = NPCSelection();
        //float visitProbability = reputation * 0.1f;
        //int rand = UnityEngine.Random.Range(1, 100);

        //if (rand <= visitProbability)
        //{
        //    GameObject npc = poolManager.SpawnFromPool(); //필요한 게임오브젝트 매개변수 처리
        //    npc.SetActive(true);
        //    Debug.Log(npcNameTag + "손님이 왕이다");
        //    NPCNum += 1;

        //}
    }

    string NPCSelection()
    {
        int rand = UnityEngine.Random.Range(1, 4);
        string npcNameTag = "NPC"+rand;


        return npcNameTag;

    }
}
