using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    public static IngredientManager instance;
    
    

    private void Awake()
    {
        instance = this;
    }
}
