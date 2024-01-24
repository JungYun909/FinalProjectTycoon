using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class ItemSlot
{
    public IngredientData item;
    public int quantity;
    public string name;
}
public class Inventory : MonoBehaviour
{
    public ItemSlotUI[] doughUiSlots;
    public IngredientSlotUI[] ingredientUiSlots;
    
    public ItemSlot[] doughSlots;
    public ItemSlot[] ingredientSlots;
    
    public bool isSet;
    
    public void StartSet()
    {
        if (isSet == false)
        {
            if (InstallationManager.instance.installationSetUI.GetComponentsInChildren<ItemSlotUI>() != null)
            {
                doughUiSlots = InstallationManager.instance.installationSetUI.GetComponentsInChildren<ItemSlotUI>();
                doughSlots = new ItemSlot[doughUiSlots.Length];
                
                for (int i = 0; i < doughSlots.Length; i++)
                {
                    doughSlots[i] = new ItemSlot();
                    doughUiSlots[i].index = i;
                    doughUiSlots[i].Clear();
                }
            }

            if (InstallationManager.instance.installationSetUI.GetComponentsInChildren<IngredientSlotUI>() != null)
            {
                ingredientUiSlots = InstallationManager.instance.installationSetUI.GetComponentsInChildren<IngredientSlotUI>();
                ingredientSlots = new ItemSlot[ingredientUiSlots.Length];

                for (int i = 0; i < ingredientSlots.Length; i++)
                {
                    ingredientSlots[i] = new ItemSlot();
                    ingredientUiSlots[i].index = i;
                    ingredientUiSlots[i].Clear();
                }
            }
            
            InstallationManager.instance.installationSetUI.SetActive(false);
            
            isSet = true;
        }
    }

    public void AddDough(IngredientData item)
    {
        if(item.canStack)
        {
            ItemSlot slotToStackTo = GetDoughStack(item);
            if(slotToStackTo != null)
            {
                slotToStackTo.quantity++;
                if(InstallationManager.instance.curInstallation == gameObject)
                    UpdateDoughUI();
                return;
            }
        }
    
        ItemSlot emptySlot = GetDoughEmptySlot();
    
        if(emptySlot != null)
        {
            emptySlot.item = item;
            emptySlot.quantity = 1;
            emptySlot.name = item.name;
            if(InstallationManager.instance.curInstallation == gameObject)
                UpdateDoughUI();
            return;
        }
        
        Debug.Log("칸이 없어요");
    }
    public void AddIngredient(IngredientData item)
    {
        if(item.canStack)
        {
            ItemSlot slotToStackTo = GetIngredientStack(item);
            if(slotToStackTo != null)
            {
                slotToStackTo.quantity++;
                if(InstallationManager.instance.curInstallation == gameObject)
                    UpdateIngredientUI();
                return;
            }
        }
    
        ItemSlot emptySlot = GetIngredientEmptySlot();
    
        if(emptySlot != null)
        {
            emptySlot.item = item;
            emptySlot.quantity = 1;
            emptySlot.name = item.name;
            if(InstallationManager.instance.curInstallation == gameObject)
                UpdateIngredientUI();
            return;
        }
        
        Debug.Log("칸이 없어요");
    }
    
    public void RemoveDough()
    {
        // 첫 번째 칸에 있는 아이템 제거
        if (doughSlots[0].item != null)
        {
            doughSlots[0].item = null;
            doughSlots[0].quantity = 0;
            doughSlots[0].name = null;
        }

        // 나머지 칸에 있는 아이템 앞으로 땡겨오기
        for (int i = 1; i < doughSlots.Length; i++)
        {
            // 현재 칸에 아이템이 있으면 앞으로 당겨오기
            if (doughSlots[i].item != null)
            {
                doughSlots[i - 1].item = doughSlots[i].item;
                doughSlots[i - 1].quantity = doughSlots[i].quantity;
                doughSlots[i - 1].name = doughSlots[i].name;

                // 현재 칸 비우기
                doughSlots[i].item = null;
                doughSlots[i].quantity = 0;
                doughSlots[i].name = null;
            }
        }
        
        if(InstallationManager.instance.curInstallation == gameObject)
            UpdateDoughUI();
    }
    
    public void RemoveIngredient()
    {
        if(ingredientSlots[0].quantity < 2)
        {
            // 첫 번째 칸에 있는 아이템 제거
            if (ingredientSlots[0].item != null)
            {
                ingredientSlots[0].item = null;
                ingredientSlots[0].quantity = 0;
                ingredientSlots[0].name = null;
            }

            // 나머지 칸에 있는 아이템 앞으로 땡겨오기
            for (int i = 1; i < ingredientSlots.Length; i++)
            {
                // 현재 칸에 아이템이 있으면 앞으로 당겨오기
                if (ingredientSlots[i].item != null)
                {
                    ingredientSlots[i - 1].item = ingredientSlots[i].item;
                    ingredientSlots[i - 1].quantity = ingredientSlots[i].quantity;
                    ingredientSlots[i - 1].name = ingredientSlots[i].name;

                    // 현재 칸 비우기
                    ingredientSlots[i].item = null;
                    ingredientSlots[i].quantity = 0;
                    ingredientSlots[i].name = null;
                }
            }
        }
        else
        {
            if (ingredientSlots[0].item != null)
            {
                ingredientSlots[0].quantity -= 1;
            }
        }
        
    }
    
    public void UpdateDoughUI()
    {
        for(int i = 0; i< doughSlots.Length; i++)
        {
            if (doughSlots[i].item != null)
                doughUiSlots[i].Set(doughSlots[i]);
            else
                doughUiSlots[i].Clear();
        }
    }
    
    public void UpdateIngredientUI()
    {
        for(int i = 0; i< ingredientSlots.Length; i++)
        {
            if (ingredientSlots[i].item != null)
                ingredientUiSlots[i].Set(ingredientSlots[i]);
            else
                ingredientUiSlots[i].Clear();
        }
    }

    ItemSlot GetDoughStack(IngredientData item)
    {
        for (int i = 0; i < doughSlots.Length; i++)
        {
            if (doughSlots[i].item == item && doughSlots[i].quantity < item.maxStackAmount)
                return doughSlots[i];
        }
    
        return null;
    }
    
    ItemSlot GetIngredientStack(IngredientData item)
    {
        for (int i = 0; i < ingredientSlots.Length; i++)
        {
            if (ingredientSlots[i].item == item && ingredientSlots[i].quantity < item.maxStackAmount)
                return ingredientSlots[i];
        }
    
        return null;
    }

    ItemSlot GetDoughEmptySlot()
    {
        for (int i = 0; i < doughSlots.Length; i++)
        {
            if (doughSlots[i].item == null)
                return doughSlots[i];
        }

        return null;
    }
    
    ItemSlot GetIngredientEmptySlot()
    {
        for (int i = 0; i < ingredientSlots.Length; i++)
        {
            if (ingredientSlots[i].item == null)
                return ingredientSlots[i];
        }

        return null;
    }
}
