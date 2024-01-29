using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ItemSlotInfo : MonoBehaviour      // 여기서 itemIcon, quantityText만 설정하고 업데이트는 UI에? 
{
    public Image itemIcon; // 아이템 아이콘을 표시할 Image 컴포넌트
    public TextMeshProUGUI quantityText; // 수량을 표시할 TextMeshProUGUI 컴포넌트

    public event Action<ItemSO> DeliverItem;

    private ItemSO curItem;

    public void Setup(ItemSO item, int quantity)
    {
        // 아이템 아이콘 설정
        if (item != null && item.sprite != null)
        {
            itemIcon.sprite = item.sprite;
            itemIcon.enabled = true; // 아이콘이 있다면 활성화
        }
        else
        {
            itemIcon.enabled = false; // 아이콘이 없다면 비활성화
        }
        curItem = item;
        // 수량 텍스트 설정
        quantityText.text = quantity > 0 ? quantity.ToString() : "";
    }
 
    public void OnButtonClicked()
    {
        DeliverItem?.Invoke(curItem);
        Debug.Log(curItem.itemName);
    }
}