using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    private List<Dictionary<string, object>> data_Resipe = null;
    
    private enum resipeType
    {
        ID,
        CombinationType,
    }
    private void Awake()
    {
        data_Resipe = CSVReader.Read("Recipe");
    }

    // public int CompareWithResipe()
    // {
    //     
    // }

    // private int SearchResipe(int resipe)
    // {
    //     for (int i = 0; i < data_Resipe.Count; i++)
    //     {
    //         if(data_Resipe[i][resipeType.CombinationType.ToString().Split(',').])
    //     }
    // }
    
}
