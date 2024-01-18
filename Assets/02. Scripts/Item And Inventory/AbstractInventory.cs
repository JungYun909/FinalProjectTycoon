using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInventory : MonoBehaviour
{
    protected InventoryManager inventoryManager;
    protected int inventoryID;

    protected virtual void Awake()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();   //TODO 추후 인벤토리매니저가 싱글톤화된 게임매니저에 통합시 변경될 부분
        inventoryID = inventoryManager.RegisterInventory(this);    // 인벤토리매니저의 RegisterInventory를 불러 자신을 등록.
    }

    public abstract void AddItem(ItemSO item, int quantity);   // 각 인벤토리 '내에' 아이템을 넣기 위한 로직
    public abstract bool RemoveItem(ItemSO item, int quantity);  // 각 인벤토리에서 아이템을 제거하기 위한 로직. 뺄수 없는 경우가 생길 수 있으므로 bool값 반환.

    // TODO 추가로직, 필요시
}