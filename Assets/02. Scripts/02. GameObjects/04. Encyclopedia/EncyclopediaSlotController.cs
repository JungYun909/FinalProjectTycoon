using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaSlotController : MonoBehaviour
{
    public ItemSO data;
    public Image image;
    public string nameTxt;
    public string recipe;
    public string price;

    public event Action<string, string, string> OnRecipeBtn;

    private void Start()
    {
        InitSetting();
        GameManager.instance.recipeManager.OnCompareRecipe += EncyclopediaUpdate;
    }
    private void OnDisable()
    {
        GameManager.instance.recipeManager.OnCompareRecipe -= EncyclopediaUpdate;
    }
    public void OnBtnClick()
    {
        OnRecipeBtn?.Invoke(nameTxt, recipe, price);
    }

    private void EncyclopediaUpdate(int index)
    {
        if (data.id - 1000 == index)
        {
            image.color = Color.white;
            nameTxt = data.itemName;
            recipe = data.recipe;
            price = data.price.ToString();
        }
    }

    public void InitSetting()
    {
        image.sprite = data.sprite;
        if (!GameManager.instance.dataManager.playerData.recipeIndex.Contains(data.id - 1000))
        {
            image.color = Color.gray;
            nameTxt = "???";
            recipe = "???";
            price = "???";

        }
        else
        {
            nameTxt = data.itemName;
            recipe = data.recipe;
            price = data.price.ToString();
        }
    }
}
