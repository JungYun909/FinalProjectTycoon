using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryObjectA : MonoBehaviour, IInteractable
{
    private UIManager uiManager;
    private AbstractInventory inventoryA;

    [SerializeField] public UIBase uiPrefab;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        inventoryA = GetComponent<AbstractInventory>();    
    }

    public void OnInteract()
    {
        //uiManager.UpdateInventoryUI(inventoryA.Items);
        uiManager.OpenWindow(uiPrefab);
    }
}
