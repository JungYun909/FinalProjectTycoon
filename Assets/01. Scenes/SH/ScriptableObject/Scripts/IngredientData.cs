using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient Data", menuName = "IngredientSO", order = 0)]
public class IngredientData : ScriptableObject
{
    [Header("Info")] 
    public string name;
    public string discription;
    public Sprite sprite;
    public string tag;

    [Header("Movement")]
    public bool canMove;
    public float moveSpeed;
    
    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;
}
