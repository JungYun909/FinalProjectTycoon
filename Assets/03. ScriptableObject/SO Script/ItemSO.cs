using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemData000", menuName = "SO by BW/Item", order = 0)]
public class ItemSO : ScriptableObject
{
    public int id; //아이템 ID (호출 넘버)
    public int type; // 아이템 타입 (재료 1 판매품2)
    public string itemname; //출력할 아이템 이름
    public string description; //출력할 아이템 설명
    public int speed; //아이템의 스피드
    public Sprite sprite;

    public int price; //해당 아이템 가격

}
