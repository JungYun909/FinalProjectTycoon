using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NpcData00", menuName = "SO by BW/NPC", order = 1)]
public class NpcSO : ScriptableObject
{
    [Header("Info")]
    public int id; //NPC ID (호출 넘버)
    public string npcName; //출력할 NPC 이름
    public string description; //출력할 아이템 설명
    public Sprite sprite;

    [Header("Movement")]
    public float speed;
    
    [Header("Buying")]
    public List<ItemSO> favoriteFood; //좋아하는 빵 (종류수 증가를 고려하여 List 선언)
}
