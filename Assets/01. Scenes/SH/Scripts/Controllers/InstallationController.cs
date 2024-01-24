using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InstallationController : MonoBehaviour, IInteractable
{
    public InstallationData _installationData;
    // public GameObject curSpawnObject;
    // private float spawnTimer;

    public bool Continuous()
    {
        return false;
    }

    public void OnClickInteract()
    {
        GameObject curInstallationSetObj = InstallationManager.instance.curInstallation;

        if (!curInstallationSetObj)
        {
            InstallationManager.instance.curInstallation = gameObject;
            InstallationManager.instance.installationSetUI.SetActive(true);
        }
        else if (curInstallationSetObj == gameObject)
        {
            InstallationManager.instance.installationSetUI.SetActive(true);
        }
        else
        {
            InstallationManager.instance.curInstallation.GetComponent<InstallationController>()._installationData.destinationInstallation =
                gameObject; //관리중인 오브젝트의 목표지로 설정
            InstallationManager.instance.installationSetUI.SetActive(true);
        }
    }

    public void OnColliderInteract()
    {
        Debug.Log("부딧혔어");
    }
    private void Start()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = _installationData.sprite;
        // if (_installationData.stat.haveMinigame)
        // {
        //     UIManagerTemp.instance.minigameSlider.value = UIManagerTemp.instance.minigameSlider.maxValue;
        // }
        // if(_installationData.stat.haveInventory)
        // {
        //     _installationData.OnClickInteract();
        //     UIManagerTemp.instance.installationSetUI.SetActive(false);
        //     InteractionManager.instance.curGameObject = null;
        //     InteractionManager.instance.targetGameObject = null;
        // }
    }

    // private void Update()
    // {
    //     spawnTimer += Time.deltaTime;
    //     
    //     if (spawnTimer > _installationData.stat.spawnDelay + (_installationData.stat.spawnDelay * ((1-_installationData.stat.curGauge / 100))))
    //     {
    //         spawnTimer = 0;
    //
    //         if (_installationData.stat.destinationInstallation)
    //         {
    //             
    //             //인벤토리가 없다면 설치물중 소환물이고 인벤토리가 있다면 제조물이다
    //             if (!_installationData.stat.haveInventory)
    //             {
    //                 curSpawnObject = PoolManager.instacne.SpawnFromPool(_installationData.stat.spawnPrefab);
    //                 curSpawnObject.transform.position = gameObject.transform.position + ((_installationData.stat.destinationInstallation.transform.position - gameObject.transform.position).normalized);
    //             }
    //             else if(_installationData.stat.installationInventory.Count > 0)
    //             {
    //                 if(!_installationData.stat.haveIngredientInventory)
    //                 {
    //                     curSpawnObject = _installationData.stat.installationInventory.Dequeue();
    //                     curSpawnObject.SetActive(true);
    //                     curSpawnObject.transform.position = gameObject.transform.position + ((_installationData.stat.destinationInstallation.transform.position - gameObject.transform.position).normalized);
    //
    //                     //현제 오브젝트의 인벤토리를 열고 있다면 유아이를 업데이트 한다
    //                     _installationData.stat.inventory.RemoveItem();
    //                     if (InteractionManager.instance.curGameObject == gameObject)
    //                     {
    //                         _installationData.stat.inventory.UpdateUI();
    //                     }
    //                 }
    //                 else if(_installationData.stat.installationIngredientInventory.Count > 0)
    //                 {
    //                     curSpawnObject = _installationData.stat.installationInventory.Dequeue();
    //                     curSpawnObject.SetActive(true);
    //                     // 반죽에 재료이미지 추가
    //                     curSpawnObject.transform.position = gameObject.transform.position + ((_installationData.stat.destinationInstallation.transform.position - gameObject.transform.position).normalized);
    //
    //                     //현제 오브젝트의 인벤토리를 열고 있다면 유아이를 업데이트 한다
    //                     _installationData.stat.inventory.RemoveItem();
    //                     _installationData.stat.ingredientInventory.RemoveItem();
    //                     if(InteractionManager.instance.curGameObject == gameObject)
    //                     {
    //                         _installationData.stat.inventory.UpdateUI();
    //                         _installationData.stat.ingredientInventory.UpdateUI();
    //                     }
    //                 }
    //                 else
    //                 {
    //                     Debug.Log("재료가 부족합니다");
    //                     return;
    //                 }
    //
    //                 
    //
    //             }
    //
    //             if (curSpawnObject.GetComponent<IngredientData>() != null)
    //             {
    //                 curSpawnObject.GetComponent<IngredientData>().InitSetting();
    //                 curSpawnObject.GetComponent<IngredientData>().stat.VisitGameObjects.Add(gameObject);
    //             }
    //
    //             if (curSpawnObject.GetComponent<MovementController>() != null)
    //             {
    //                 curSpawnObject.GetComponent<MovementController>().Move(_installationData.stat.destinationInstallation);
    //             }
    //
    //
    //         }
    //     }
    //     //스폰하는 게이지 업데이트
    //     float curSpawnDelay =_installationData.stat.spawnDelay + (_installationData.stat.spawnDelay * (1 - (_installationData.stat.curGauge / 100)));
    //     UIManagerTemp.instance.spawnSlider.maxValue = curSpawnDelay;
    //     UIManagerTemp.instance.spawnSlider.value = spawnTimer;
    //     
    //     //미니게임 온도가 초당 0.1 퍼센트 감소 값 설정
    //     float decreaseAmount = UIManagerTemp.instance.minigameSlider.maxValue * (_installationData.stat.decreaseTime / 100f);
    //     //설정값 만큼 감소, 최솟값 설정
    //     UIManagerTemp.instance.minigameSlider.value -= decreaseAmount * Time.deltaTime;
    //     UIManagerTemp.instance.minigameSlider.value = Mathf.Max(UIManagerTemp.instance.minigameSlider.value, 0f);
    //     //미니게임이 있다면 현제 게이지를 온도계의 퍼센티지만큼 감소
    //     if (_installationData.stat.haveMinigame)
    //     {
    //         _installationData.stat.curGauge = Mathf.Lerp(0f, 100f, UIManagerTemp.instance.minigameSlider.normalizedValue);
    //     }
    // }

}
