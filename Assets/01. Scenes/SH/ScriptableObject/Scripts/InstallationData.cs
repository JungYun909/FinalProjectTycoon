using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct InstallationStat
{

    // [Header("Spawn")]
    // public bool canSpawn;
    // public GameObject spawnPrefab;
    // public float spawnDelay;
    //
    // [Header("Inventory")] 
    // public bool haveInventory;
    // public GameObject curInventoryItem;
    // public Inventory inventory;
    // public Queue<GameObject> installationInventory;
    //
    // [Header("IngredientInventory")]
    // public bool haveIngredientInventory;
    // public GameObject curIngredientInventoryItem;
    // public IngredientInventory ingredientInventory;
    // public Queue<GameObject> installationIngredientInventory;
    //
    // [Header("Minigame")]
    // public bool haveMinigame;
    // public float curGauge;
    // public float decreaseTime;
}
[CreateAssetMenu(fileName = "Installation Data", menuName = "InstallationSO", order = 0)]
public class InstallationData : ScriptableObject
{
    [Header("Info")] 
    public string name;
    public string discription;
    public Sprite sprite;

    [Header("Destination")]
    public GameObject destinationInstallation;

    [Header("Spawning")]
    public bool canSpawn;
    public float spawnDelay;

    // public bool Continuous()
    // {
    //     return false;
    // }

    // public virtual void OnClickInteract()
    // {
    //     InstallationManager.instance.installationSetUI.SetActive(true);
    //     // //상호작용 매니저에 현제 상호작용중인 객체가 없을때 설정
    //     // if (InteractionManager.instance.curGameObject == null || InteractionManager.instance.curGameObject == gameObject)
    //     // {
    //     //     InteractionManager.instance.curGameObject = gameObject; //상호작용 매니저에게 상호작용 중임을 선언
    //     //     UIManagerTemp.instance.installationSetUI.SetActive(true); //유아이 매니저가 관리하고 있는 설치물 관리 UI 사용 선언
    //     //
    //     //     //인벤토리 가지고 있을경우 세팅
    //     //     if (!stat.haveInventory)
    //     //     {
    //     //         UIManagerTemp.instance.inventoryUI.SetActive(false);
    //     //     }
    //     //     else
    //     //     {
    //     //         UIManagerTemp.instance.inventoryUI.SetActive(true);
    //     //         if (stat.haveInventory)
    //     //         {
    //     //             stat.inventory.StartSet();
    //     //         }
    //     //     }
    //     //     //재료인벤토리를 가지고 있을경우 세팅
    //     //     if (!stat.haveIngredientInventory)
    //     //     {
    //     //         UIManagerTemp.instance.ingredientInventoryUI.SetActive(false);
    //     //     }
    //     //     else
    //     //     {
    //     //         UIManagerTemp.instance.ingredientInventoryUI.SetActive(true);
    //     //         if (stat.haveIngredientInventory)
    //     //         {
    //     //             stat.ingredientInventory.StartSet();
    //     //         }
    //     //     }
    //     //     //미니게임 가지고 있을경우 세팅
    //     //     if (!stat.haveMinigame)
    //     //     {
    //     //         UIManagerTemp.instance.minigameUI.SetActive(false);
    //     //     }
    //     //     else
    //     //     {
    //     //         UIManagerTemp.instance.minigameUI.SetActive(true);
    //     //     }
    //     // }
    //     // else if(InteractionManager.instance.curGameObject != gameObject)
    //     // {
    //     //     InteractionManager.instance.curGameObject.GetComponent<InstallationData>().stat.destinationInstallation = gameObject;
    //     //     InteractionManager.instance.curGameObject.GetComponent<IInteractable>().OnClickInteract();
    //     // }
    //     // //상호작용중인 객체가 있을때 설정
    // }
    //
    // public virtual void OnColliderInteract()
    // {
    //     // if (stat.haveInventory)
    //     // {
    //     //     if(stat.curInventoryItem != null)
    //     //     {
    //     //         if (stat.installationInventory.Count < 5)
    //     //         {
    //     //             stat.installationInventory.Enqueue(stat.curInventoryItem);
    //     //             stat.inventory.AddItem(stat.curInventoryItem.GetComponent<IngredientData>());
    //     //             if (InteractionManager.instance.curGameObject == gameObject)
    //     //             {
    //     //                 stat.inventory.UpdateUI();
    //     //             }
    //     //         }
    //     //         stat.curInventoryItem = null;
    //     //     }
    //     //
    //     //     if(stat.curIngredientInventoryItem != null && stat.haveIngredientInventory)
    //     //     {
    //     //         stat.installationIngredientInventory.Enqueue(stat.curIngredientInventoryItem);
    //     //         stat.ingredientInventory.AddItem(stat.curIngredientInventoryItem.GetComponent<IngredientData>());
    //     //         if (InteractionManager.instance.curGameObject == gameObject)
    //     //         {
    //     //             stat.ingredientInventory.UpdateUI();
    //     //         }
    //     //         stat.curIngredientInventoryItem = null;
    //     //     }
    //     // }
    // }
}
