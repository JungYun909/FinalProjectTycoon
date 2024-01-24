using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientSlotUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI quatityText;
    public TextMeshProUGUI nameText;
    private IngredientSlot curSlot;

    public int index;
    public void Set(IngredientSlot slot)
    {
        curSlot = slot;
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.stat.icon;
        quatityText.text = slot.quantity > 1 ? slot.quantity.ToString() : string.Empty;
        nameText.text = slot.quantity >= 1 ? slot.name.ToString() : string.Empty;
    }

    public void Clear()
    {
        curSlot = null;
        icon.gameObject.SetActive(false);
        quatityText.text = string.Empty;
    }
}
