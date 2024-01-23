using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ItemSlot
{
    public IngredientData item;
    public int quantity;
    public string name;
}
public class Inventory : MonoBehaviour
{
    public ItemSlotUI[] uiSlots;
    public ItemSlot[] itemSlots;
    public bool isSet;
    
    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    private int curEquipIndex;

    public void StartSet()
    {
        if (isSet == false)
        {
            uiSlots = UIManagerTemp.instance.installationSetUI.GetComponentsInChildren<ItemSlotUI>();
            Debug.Log(uiSlots.Length);
            UIManagerTemp.instance.installationSetUI.SetActive(false);
            itemSlots = new ItemSlot[uiSlots.Length];

            for (int i = 0; i < itemSlots.Length; i++)
            {
                itemSlots[i] = new ItemSlot();
                uiSlots[i].index = i;
                uiSlots[i].Clear();
            }

            isSet = true;
        }
        
        UpdateUI();
    }

    public void OnInventoryButton()
    {
        Toggle();
    }


    public void Toggle()
    {
        if (UIManagerTemp.instance.installationSetUI.activeInHierarchy)
        {
            UIManagerTemp.instance.installationSetUI.SetActive(false);
        }
        else
        {
            UIManagerTemp.instance.installationSetUI.SetActive(true);
        }
    }

    public bool IsOpen()
    {
        return UIManagerTemp.instance.installationSetUI.activeInHierarchy;
    }

    public void AddItem(IngredientData item)
    {
        if(item.stat.canStack)
        {
            ItemSlot slotToStackTo = GetItemStack(item);
            if(slotToStackTo != null)
            {
                slotToStackTo.quantity++;
                return;
            }
        }

        ItemSlot emptySlot = GetEmptySlot();

        if(emptySlot != null)
        {
            emptySlot.item = item;
            emptySlot.quantity = 1;
            emptySlot.name = item.stat.name;
            return;
        }
        
        Debug.Log("칸이 없어요");
    }

    public void UpdateUI()
    {
        for(int i = 0; i< itemSlots.Length; i++)
        {
            if (itemSlots[i].item != null)
                uiSlots[i].Set(itemSlots[i]);
            else
                uiSlots[i].Clear();
        }
    }

    ItemSlot GetItemStack(IngredientData item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == item && itemSlots[i].quantity < item.stat.maxStackAmount)
                return itemSlots[i];
        }

        return null;
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == null)
                return itemSlots[i];
        }

        return null;
    }
    
    public void RemoveItem()
    {
        // 첫 번째 칸에 있는 아이템 제거
        if (itemSlots[0].item != null)
        {
            itemSlots[0].item = null;
            itemSlots[0].quantity = 0;
            itemSlots[0].name = null;
        }

        // 나머지 칸에 있는 아이템 앞으로 땡겨오기
        for (int i = 1; i < itemSlots.Length; i++)
        {
            // 현재 칸에 아이템이 있으면 앞으로 당겨오기
            if (itemSlots[i].item != null)
            {
                itemSlots[i - 1].item = itemSlots[i].item;
                itemSlots[i - 1].quantity = itemSlots[i].quantity;
                itemSlots[i - 1].name = itemSlots[i].name;

                // 현재 칸 비우기
                itemSlots[i].item = null;
                itemSlots[i].quantity = 0;
                itemSlots[i].name = null;
            }
        }
    }
}
