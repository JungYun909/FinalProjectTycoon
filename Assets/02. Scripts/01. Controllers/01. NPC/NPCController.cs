using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class NPCController : MonoBehaviour
{
    public NpcSO curNPCData;
    public SpriteRenderer npcImage;
    public List<GameObject> visitObj;
    public GameObject destinationObj;
    public ItemSO favoriteFood;

    public GameObject whatToBuy;
    public SpriteRenderer favoriteFoodIcon;
    private int paymentAmount;
    public bool visitCounter;
    public bool buy;
    
    public NPCDestinationSet destinationController;
    public MovementController movementController;

    private void OnEnable()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != SceneType.MainScene.ToString())
        {
            return;
        }
        
        GameManager.instance.dataManager.OnSaveEvent += NewInitSetting;
    }

    private void OnDisable()
    {
        GameManager.instance.dataManager.OnSaveEvent -= NewInitSetting;
    }

    private void NewInitSetting()
    {
        movementController.Reset();
        InitSetting();
    }

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
        
        npcImage.sprite = curNPCData.sprite;
        whatToBuy.SetActive(true);
        
        foreach (var installation in GameManager.instance.dataManager.curInstallations)
        {
            if (installation != null && installation.GetComponent<InstallationController>()._installationData.id == 5)
            {
                visitObj.Add(installation);
            }
        }

        if (GameManager.instance.dataManager.playerData.day > 2)
            favoriteFood = curNPCData.favoriteFood[Random.Range(0, curNPCData.favoriteFood.Count)];
        else
            favoriteFood = GameManager.instance.inventoryManager.itemDatabase.GetItemByID(1001);
        favoriteFoodIcon.sprite = favoriteFood.sprite;
        destinationController.MachinePosInform();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject != movementController.destinationObj)
            return;

        movementController.Reset();
        visitObj.Remove(other.gameObject);

        if (other.gameObject == destinationObj)
        {
            BuyItem(other.gameObject);
        }
        
        if (other.gameObject == GameManager.instance.dataManager.counter)
        {
            GameManager.instance.statManager.EarnGold(paymentAmount);
        }
        
        if (other.gameObject == GameManager.instance.dataManager.entrance)
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
            return;
        }

        foreach (var item in inventory.Items)
        {
            if (item.Key == favoriteFood)
            {
                buy = true;
                whatToBuy.SetActive(false);
                GameManager.instance.inventoryManager.RemoveItemFromInventory(inventory.inventoryID, item.Key, 1);
                visitObj.Clear();
                paymentAmount = favoriteFood.price;
                break;
            }
        }
    }
}
