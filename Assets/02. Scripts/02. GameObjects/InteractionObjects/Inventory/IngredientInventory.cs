using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSlot
{
    public IngredientData item;
    public int quantity;
    public string name;
}

public class IngredientInventory : MonoBehaviour
{
    public IngredientSlotUI[] uiSlots;
    public IngredientSlot[] ingredientSlots;
    public bool isSet;

    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    private int curEquipIndex;

    public void StartSet()
    {
        if (isSet == false)
        {
            uiSlots = UIManagerTemp.instance.installationSetUI.GetComponentsInChildren<IngredientSlotUI>();
            Debug.Log(uiSlots.Length);
            UIManagerTemp.instance.installationSetUI.SetActive(false);
            ingredientSlots = new IngredientSlot[uiSlots.Length];

            for (int i = 0; i < ingredientSlots.Length; i++)
            {
                ingredientSlots[i] = new IngredientSlot();
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
        if (item.stat.canStack)
        {
            IngredientSlot slotToStackTo = GetItemStack(item);
            if (slotToStackTo != null)
            {
                slotToStackTo.quantity++;
                return;
            }
        }

        IngredientSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
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
        for (int i = 0; i < ingredientSlots.Length; i++)
        {
            if (ingredientSlots[i].item != null)
                uiSlots[i].Set(ingredientSlots[i]);
            else
                uiSlots[i].Clear();
        }
    }

    IngredientSlot GetItemStack(IngredientData item)
    {
        for (int i = 0; i < ingredientSlots.Length; i++)
        {
            if (ingredientSlots[i].item == item && ingredientSlots[i].quantity < item.stat.maxStackAmount)
                return ingredientSlots[i];
        }

        return null;
    }

    IngredientSlot GetEmptySlot()
    {
        for (int i = 0; i < ingredientSlots.Length; i++)
        {
            if (ingredientSlots[i].item == null)
                return ingredientSlots[i];
        }

        return null;
    }

    public void RemoveItem()
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
}
