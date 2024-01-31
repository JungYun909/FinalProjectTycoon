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
    
    
    public NPCDestinationSet destinationController;
    public MovementController movementController;
    public bool visitCounter;
    public bool buy;
    

    public void InitSetting()
    {
        visitCounter = false;
        buy = false;
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
        movementController.destinationObj = null;
        movementController.isMove = false;

        if (other.gameObject == destinationObj)
        {
            AbstractInventory inventory = other.gameObject.GetComponent<AbstractInventory>();

            foreach (var item in inventory.Items)
            {
                if (item.Key == favoriteFood)
                {
                    buy = true;
                    GameManager.instance.inventoryManager.RemoveItemFromInventory(inventory.inventoryID, item.Key, 1);
                }
            }
        }
        
        if (other.gameObject == GameManager.instance.dataManager.counter)
        {
            // GameManager.instance.statManager.EarnGold()
        }
        
        if (other.gameObject == GameManager.instance.spawnManager.NPCSpawnObj)
        {
            GameManager.instance.poolManager.DeSpawnFromPool(gameObject);
            GameManager.instance.spawnManager.curNpcCount--;
        }

        destinationController.StartCoroutine();
    }
}
