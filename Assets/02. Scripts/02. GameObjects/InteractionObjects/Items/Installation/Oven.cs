using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System.Threading;

public class Oven : InteractionData
{
    public Sprite curIcon;
    public GameObject destinationObj;
    public GameObject installationUI;
    public GameObject installUI;
    public GameObject SpawnPrefab;
    public override void InitSetting()
    {
        _interactionStat.name = "Oven";
        _interactionStat.description = "Bake until golden brown";
        _interactionStat.type = InteractionType.Installation;
        _interactionStat.icon = curIcon;
        _interactionStat.canCell = true;
        _interactionStat.price = 100000;
        _interactionStat.canMove = false;
        _interactionStat.speed = 0f;
        
        installationUI = UIManagerTemp.instance.installationSetUI;
        installUI = UIManagerTemp.instance.installUI;
    }

    public override bool Continuous()
    {
        return false;
    }

    public override void OnInteract()
    {
        if (InteractionManager.instance.curGameObject == gameObject)
        {
            InteractionManager.instance.curGameObject = gameObject;
            
            installationUI.SetActive(true);
            installUI.gameObject.GetComponentInChildren<InstallationMoveController>().curGameObject = gameObject;
            
            InstallationBtnController[] installationBtnControllers = installUI.gameObject.GetComponentsInChildren<InstallationBtnController>();
            foreach (var btns in installationBtnControllers)
            {
                btns.installationGameObject = gameObject;
            }

            if (InteractionManager.instance.targetGameObject)
            {
                destinationObj = InteractionManager.instance.targetGameObject;
                StartCoroutine("SpawnPrefabContinuously");
            }
        }
        else
        {
            InteractionManager.instance.targetGameObject = gameObject;
        }
    }
    
    
    private IEnumerator SpawnPrefabContinuously()
    {
        while (destinationObj != null)
        {
            yield return new WaitForSeconds(1f); // 적절한 시간 간격을 설정할 수 있음

            GameObject newSpawnObject = PoolManager.instacne.SpawnFromPool(SpawnPrefab);
            newSpawnObject.transform.position = gameObject.transform.position;
        }
    }
}
