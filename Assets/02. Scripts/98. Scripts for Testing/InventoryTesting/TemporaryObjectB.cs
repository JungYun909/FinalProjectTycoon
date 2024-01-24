using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryObjectB : MonoBehaviour, IInteractable
{
    private UIManager uiManager;
    private AbstractInventory inventoryB;

    [SerializeField] public UIBase uiPrefab;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        inventoryB = GetComponent<AbstractInventory>();
    }

    public void OnInteract()
    {
        //uiManager.UpdateInventoryUI(inventoryB.Items);
        uiManager.OpenWindow(uiPrefab);
    }
}
