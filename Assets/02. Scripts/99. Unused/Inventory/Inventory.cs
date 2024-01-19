using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ItemSlot
{
    public ItemData item;
    public int quantity;
    public string name;
}
public class Inventory : MonoBehaviour
{
    public ItemSlotUI[] uiSlots; //TODO UI Manager 기능으로 전환
    public ItemSlot[] slots;

    public GameObject inventoryWindow;
    
    // [Header("Strawberry")] //TODO ItemDataManager 아이템 매니저의 값을 받아서 생성
    // public ItemObject strawberry;
    // public ItemObject strawberryTang;
    //
    // [Header("Orange")]
    // public ItemObject orange;
    // public ItemObject orangeTang;
    //
    // [Header("Grape")]
    // public ItemObject grape;
    // public ItemObject grapeTang;

    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    private int curEquipIndex;

    // [Header("Events")]
    // public UnityEvent onOpenInventory;
    // public UnityEvent onCloseInventory;

    public static Inventory instance; //아래와 같음
    void Awake() //InventoryManager로 습급시 삭제 GameManager가 실행
    {
        instance = this;
    }
    private void Start() //TODO 스타트가 아닌 메소드로 구현 Inventory 에서 상속받아서 구현 abstract
    {
        inventoryWindow.SetActive(false);
        slots = new ItemSlot[uiSlots.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new ItemSlot();
            uiSlots[i].index = i;
            uiSlots[i].Clear();
        }
    }

    // public void OnInventoryButton() //TODO UIHandler 에서 인벤토리 키고 끄는 것 관리
    // {
    //     Toggle();
    // }
    //
    //
    // public void Toggle()
    // {
    //     if (inventoryWindow.activeInHierarchy)
    //     {
    //         inventoryWindow.SetActive(false);
    //         onCloseInventory?.Invoke();
    //     }
    //     else
    //     {
    //         inventoryWindow.SetActive(true);
    //         onOpenInventory?.Invoke();
    //     }
    // }

    // public bool IsOpen()
    // {
    //     return inventoryWindow.activeInHierarchy;  //TODO UIHandler 에서 정보 받아와서 Bool 값 설정
    // }

    //인벤토리 아이템 추가 기능
    public void AddItem(ItemData item)
    {
        // if(item.canStack)
        // {
        //     ItemSlot slotToStackTo = GetItemStack(item);
        //     if(slotToStackTo != null)
        //     {
        //         slotToStackTo.quantity++;
        //         UpdateUIDater();
        //         return;
        //     }
        // }

        ItemSlot emptySlot = GetEmptySlot();

        if(emptySlot != null)
        {
            emptySlot.item = item;
            emptySlot.quantity = 1;
            emptySlot.name = item.itemStat.name;
            UpdateUIDater();
            return;
        }
    }
    
    //인벤토리 아이템 바꾸기 기능
    // public void ChangeItem(ItemData item, ItemData changeItem)
    // {
    //     ItemSlot slotToStackTo = GetItemStack(item);
    //     if (slotToStackTo != null)
    //     {
    //         slotToStackTo.item = changeItem;
    //         UpdateUIDater();
    //         return;
    //     }
    // }
    //UI의 ItemData값 최신화
    void UpdateUIDater()
    {
        for(int i = 0; i< slots.Length; i++)
        {
            if (slots[i].item != null)
                uiSlots[i].Set(slots[i]);
            else
                uiSlots[i].Clear();
        }
    }
    //인벤토리에 있는 아이템 위치 찾기
    // ItemSlot GetItemStack(ItemData item)
    // {
    //     for (int i = 0; i < slots.Length; i++)
    //     {
    //         if (slots[i].item == item && slots[i].quantity < item.maxStackAmount)
    //             return slots[i];
    //     }
    //
    //     return null;
    // }
    //인벤토리 빈칸 찾기
    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
                return slots[i];
        }

        return null;
    }
}
