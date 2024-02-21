using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "MachineData00", menuName = "SO by BW/Machine", order = 2)]
public class MachineSO : ScriptableObject
{
    [Header("Info")] 
    public int id; //machine ID (호출 넘버)
    public string installasionName; //출력할 기계 이름
    public string description; //출력할 기계 설명
    public int price;
    public Sprite sprite;
    public List<AnimationClip> animation;

    [Header("Spawning")]
    public bool canSpawn;
    public float spawnDelay;
    public ItemSO spawnData;
    
    [Header("Inventory")]
    public bool haveDoughInventory;
    public bool haveIngredientInventory;
    public float makeDelay;
    public int maxSlot;

    [Header("CompleteMake")] 
    public bool completeMake;
}
