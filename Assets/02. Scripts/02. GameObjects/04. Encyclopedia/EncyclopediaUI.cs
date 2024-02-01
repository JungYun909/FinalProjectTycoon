using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncyclopediaUI : UIBase
{
    public GameObject slot;
    public Transform slotsParent;
    public override void Initialize()
    {
        return;
    }

    public override void UpdateUI()
    {
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
        }
    }
}
