using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EncyclopediaUI : UIBase
{
    public GameObject slot;
    public Transform slotsParent;
    private List<EncyclopediaSlotController>  slotController = new List<EncyclopediaSlotController>();

    public TextMeshProUGUI foodName;
    public TextMeshProUGUI foodRecipe;
    public TextMeshProUGUI foodPrice;
        
    public override void Initialize()
    {
        return;
    }

    public override void UpdateUI()
    {
        return;
    }

    public void Start()
    {
        foodName.text = "???";
        foodRecipe.text = "???";
        foodPrice.text = "???";
        
        foreach (GameObject curSlot in slotsParent)
        {
            Destroy(curSlot);
        }
        
        foreach (var itemData in GameManager.instance.dataManager.foodSub)
        {
            GameObject curSlot = Instantiate(slot, slotsParent);
            EncyclopediaSlotController controller = curSlot.GetComponent<EncyclopediaSlotController>();
            controller.data = itemData;
            controller.InitSetting();
            slotController.Add(controller);
        }

        foreach (var controller in slotController)
        {
            controller.OnRecipeBtn += DataUpdate;
        }
    }

    private void DataUpdate(string itemName, string recipe, string price)
    {
        foodName.text = itemName;
        foodRecipe.text = recipe;
        foodPrice.text = price;
    }

    public void BackBtn()
    {
        GameManager.instance.uiManager.CloseAll();
    }
}
