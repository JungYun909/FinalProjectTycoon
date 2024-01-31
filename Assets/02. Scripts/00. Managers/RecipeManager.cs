using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    private List<Dictionary<string, object>> data_Resipe = null;
    
    private enum ResipeType
    {
        ID,
        Recipe
    }
    private void Awake()
    {
        data_Resipe = CSVReader.Read("RecipeCSV - Recipe");
        Debug.Log(data_Resipe[1]["Recipe"]);
    }
    
    public int CompareWithResipe(int resipe)
    {
        for (int i = 0; i < data_Resipe.Count; i++)
        {
            if (((int)data_Resipe[i][ResipeType.Recipe.ToString()] == resipe))
            {
                return (int)data_Resipe[i][ResipeType.ID.ToString()] + 1;
            }
        }
        return 0;
    }
}
