using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemData000", menuName = "SO by BW/Item", order = 0)]
public class ItemSO : ScriptableObject
{
    [Header("Info")] 
    public int id; //아이템 ID (호출 넘버)
    public int type; // 아이템 타입 (재료 1 판매품2)
    public string itemName; //출력할 아이템 이름
    public string description; //출력할 아이템 설명
    public string recipe;
    public int price; //해당 아이템 가격
    public Sprite sprite;
    public string tag;
    
    [Header("Movement")]
    public bool canMove;
    public float moveSpeed;
    
    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;
}
