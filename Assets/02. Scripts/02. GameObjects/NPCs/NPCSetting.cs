using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSetting : MonoBehaviour
{
    [SerializeField] private NpcDatabaseSO npcDatabaseSO;
    [SerializeField] private List<NpcSO> npcDataList;
    [SerializeField] private System.Random random;

    public NpcSO npcSo;
    [SerializeField] int npcDatanum;

    void Awake()
    {
        random = new System.Random();

        // 이하 차후 onEnalbe()로 옮길 것
        int num = random.Next(0, npcDatabaseSO.npcDataList.Count + 1); ;
        for (int i = 0; i < npcDatabaseSO.npcDataList.Count; i++)
        {
            npcDataList.Add(npcDatabaseSO.npcDataList[i]);
        }

        npcSo = npcDataList[num];

    }


}
