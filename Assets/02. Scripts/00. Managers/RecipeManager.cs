using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public event Action<int> OnCompareRecipe;
    
    private List<Dictionary<string, object>> data_Resipe = null;
    
    private enum ResipeType
    {
        ID,
        Recipe
    }
    private void Awake()
    {
        data_Resipe = CSVReader.Read("RecipeCSV - Recipe");
    }
    
    public int CompareWithResipe(string resipe)
    {
        Debug.Log(resipe);
        for (int i = 0; i < data_Resipe.Count; i++)
        {
            if (data_Resipe[i][ResipeType.Recipe.ToString()].ToString() == resipe)
            {
                OnCompareRecipe?.Invoke((int)data_Resipe[i][ResipeType.ID.ToString()]);
                Debug.Log((int)data_Resipe[i][ResipeType.ID.ToString()]);
                return (int)data_Resipe[i][ResipeType.ID.ToString()];
            }
        }
        OnCompareRecipe?.Invoke(0);
        return 0;
    }
}
