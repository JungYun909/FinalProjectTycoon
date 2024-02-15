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
    public Slider timer;

    public event Action<ItemSO> DeliverItem;
    public event Action<int> DeliverInventoryID;
    public event Action DeliverInventoryInfo;
    private ItemSO curItem;
    private MachineSO curMachine;

    private InventoryShow inventoryShow;
    private InstallationInventoryController inventoryController;
    private ShopInventory shopInventory;

    private float timerDuration;
    private float curTimer = 0f;
    private bool isTimerOn = false;

    private void Awake()
    {
        inventoryShow = GetComponentInParent<InventoryShow>();
        if(inventoryShow != null) Debug.Log("InventoryShow Found");
        shopInventory = FindObjectOfType<ShopInventory>();
    }
    
    public void Setup(ItemSO item, int quantity)
    {
        timer.gameObject.SetActive(false);
        if(curMachine != null)
        timerDuration = curMachine.makeDelay;
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
        if (quantity == 1)
        {
            quantityText.text = "";
        }
    }

    public void StartTimer()
    {
        Debug.Log("Timer Started");
        timer.gameObject.SetActive(true);
        isTimerOn = true;
    }

    public void SetTimerDuration(float duration)
    {
        this.timerDuration = duration;
    }

    public void SetupMachineInfo(MachineSO machineSO)
    {
        curMachine = machineSO;
    }

    public void SetupMachineInfo(MachineSO machine, int quantity)
    {
        if (machine != null && machine.sprite != null)
        {
            itemIcon.sprite = machine.sprite;
            itemIcon.enabled = true;
        }
        else
        {
            itemIcon.enabled = false;
        }
        curMachine = machine;
    }

    private void OnEnable()
    {
        if (inventoryShow != null)
        {
            inventoryShow.DeliverInventoryID += OnButtonClicked;
            inventoryController = inventoryShow.curInventory.GetComponent<InstallationInventoryController>();
            if (inventoryController != null)
            {
                inventoryController.deliverCurrentTime += UpdateTimer;
            }
        }
        else
            return;
    }
    private void UpdateTimer(float time)
    {
        if (!isTimerOn)
            return;
        curTimer = time;
        timer.value = curTimer / timerDuration;
    }
    private void OnDisable()
    {
        if (inventoryShow != null)
        {
            inventoryShow.DeliverInventoryID -= OnButtonClicked;
            //inventoryShow.DeliverMachineInfo -= SetupMachineInfo;
            if (inventoryController != null)
                inventoryController.deliverCurrentTime -= UpdateTimer;
        }
        else
            return;
    }

    public void OnButtonClicked(int toInventoryID)
    {
        if (curItem != null)
        {
            // 중복 구독 방지를 위해 기존 구독 해제
            DeliverItem?.Invoke(curItem);
        }

        var inventoryShowInstance = FindObjectOfType<InventoryShow>();
        if (inventoryShowInstance != null && inventoryShowInstance.curInventory != null)
        {
            toInventoryID = inventoryShowInstance.curInventory.inventoryID;
            DeliverInventoryInfo?.Invoke();
            DeliverInventoryID?.Invoke(toInventoryID);
        }
        else
        {
            var standInventoryUIInstance = FindObjectOfType<StandInventoryUI>();
            if (standInventoryUIInstance != null && standInventoryUIInstance.curInventory != null)
            {
                toInventoryID = standInventoryUIInstance.curInventory.inventoryID;
                DeliverInventoryInfo?.Invoke();
                DeliverInventoryID?.Invoke(toInventoryID);
            }
            else
            {
                Debug.Log("No valid Inventory found");
                return;
            }
        }
    }
}