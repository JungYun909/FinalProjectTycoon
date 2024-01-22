using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryUIElementsUpdator : MonoBehaviour    // 추후 UIManager로 통합 필요한 스크립트.
{
    public GameObject itemSlotPrefab; // 아이템 슬롯 프리팹
    public GameObject itemLinePrefab; // 아이템 라인 프리팹
    public Transform contentPanel;    // 인벤토리 슬롯을 배치할 부모 패널

    public void UpdateUI(Dictionary<ItemSO, int> items)
    {
        // 기존 슬롯 제거
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        GameObject currentLine = null;
        int slotIndex = 0;

        foreach (var entry in items)
        {
            // 새로운 라인이 필요한 경우 생성
            if (slotIndex % 5 == 0)
            {
                currentLine = Instantiate(itemLinePrefab, contentPanel);
            }

            // 아이템 슬롯 생성 및 설정
            GameObject slotObject = Instantiate(itemSlotPrefab, currentLine.transform);
            ItemSlotInfo slotInfo = slotObject.GetComponent<ItemSlotInfo>();
            slotInfo.Setup(entry.Key, entry.Value);

            slotIndex++;
        }
    }
}