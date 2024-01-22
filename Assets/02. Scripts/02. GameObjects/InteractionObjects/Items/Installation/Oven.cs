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
    public bool isSpawn;
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
                destinationObj = InteractionManager.instance.targetGameObject;
        }
        else
        {
            InteractionManager.instance.targetGameObject = gameObject;
        }
        if(isSpawn == false && destinationObj != null)
        {
            isSpawn = true;
            while (destinationObj != null)
            {
                PoolManager.instacne.SpawnFromPool(SpawnPrefab);
                Thread.Sleep(2000);
            }
        }
    }
}
