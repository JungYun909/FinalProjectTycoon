using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSeparation : MonoBehaviour
{
    public Dictionary<int, List<NpcSO>> NPCs;

    private void Start()
    {
        NPCs = new Dictionary<int, List<NpcSO>>();
        
        foreach (NpcSO npcSo in GameManager.instance.dataManager.npcDatas.npcDataList)
        {
            if (!NPCs.ContainsKey(npcSo.npcLevel))
            {
                NPCs.Add(npcSo.npcLevel, new List<NpcSO>());
            }
            
            NPCs[npcSo.npcLevel].Add(npcSo);
        }
    }

    public NpcSO NPCChoice(int level)
    {
        int randomLv = Random.Range(1, level + 1);
        return NPCs[randomLv][Random.Range(0, NPCs[randomLv].Count)];
    }
}
