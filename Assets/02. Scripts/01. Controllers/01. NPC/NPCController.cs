using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class NPCController : MonoBehaviour
{
    public NpcSO curNPCData;
    public List<GameObject> visitObj;
    public GameObject destinationObj;
    public ItemSO favoriteFood;
    private int paymentAmount;
    public bool visitCounter;
    public bool buy;
    
    public NPCDestinationSet destinationController;
    public MovementController movementController;

    

    public void InitSetting()
    {
        visitCounter = false;
        buy = false;
        paymentAmount = 0;
        favoriteFood = null;
        destinationObj = null;
        movementController.destinationObj = null;
        movementController.isMove = false;
        visitObj.Clear();
        
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = curNPCData.sprite;
        
        foreach (var installation in GameManager.instance.dataManager.curInstallations)
        {
            if (installation.GetComponent<InstallationController>()._installationData.id == 5)
            {
                visitObj.Add(installation);
            }
        }

        favoriteFood = curNPCData.favoriteFood[Random.Range(0, curNPCData.favoriteFood.Count)];
        
        destinationController.MachinePosInform();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject != movementController.destinationObj)
            return;
        
        visitObj.Remove(other.gameObject);

        if (other.gameObject == destinationObj)
        {
            BuyItem(other.gameObject);
        }
        
        if (other.gameObject == GameManager.instance.dataManager.counter)
        {
            Debug.Log(GameManager.instance.statManager.currentGold);
            GameManager.instance.statManager.EarnGold(paymentAmount);
            Debug.Log(GameManager.instance.statManager.currentGold);
        }
        
        if (other.gameObject == GameManager.instance.spawnManager.NPCSpawnObj)
        {
            GameManager.instance.poolManager.DeSpawnFromPool(gameObject);
            GameManager.instance.spawnManager.curNpcCount--;
            return;
        }

        destinationController.StartCoroutine();
    }

    private void BuyItem(GameObject other)
    {
        AbstractInventory inventory = other.gameObject.GetComponentInChildren<AbstractInventory>();

        if (inventory.Items.Count == 0)
        {
            Debug.Log("아무것도 없다");
            return;
        }

        foreach (var item in inventory.Items)
        {
            if (item.Key == favoriteFood)
            {
                buy = true;
                GameManager.instance.inventoryManager.RemoveItemFromInventory(inventory.inventoryID, item.Key, 1);
                Debug.Log(favoriteFood.price);
                paymentAmount = favoriteFood.price;
                break;
            }
        }
    }
}
