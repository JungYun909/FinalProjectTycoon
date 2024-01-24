using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : UIBase
{

    [Header("For Inventory UI Update")]
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GameObject itemLinePrefab;
    [SerializeField] private Transform contentPanel;
    private UIManager uiManager;

    private AbstractInventory curInventory;


    private void Awake()
    {
        uiManager  = FindObjectOfType<UIManager>();
    }
    public override void Initialize()
    {
        Debug.Log("NN");
    }

    public override void UpdateUI()
    {
        Debug.Log("MM");
    }

    //public void SetInventoryInfo(AbstractInventory inventory)
    //{
    //    curInventory = inventory;
    //    UpdateUI();
    //}

}
