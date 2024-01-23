using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InstallationStat
{
    [Header("Info")] 
    public string name;
    public string discription;

    [Header("Destination")]
    public GameObject destinationInstallation;

    [Header("Spawn")]
    public bool canSpawn;
    public GameObject spawnPrefab;
    public float spawnDelay;
    public float moveSpeed;
}
public abstract class InstallationData : MonoBehaviour, IInteractable
{
    public InstallationStat stat;
    public abstract void InitSetting();
    public abstract bool Continuous();

    public virtual void OnClickInteract()
    {
        //상호작용 매니저에 현제 상호작용중인 객체가 없을때 설정
        if (InteractionManager.instance.curGameObject == null)
        {
            InteractionManager.instance.curGameObject = gameObject; //상호작용 매니저에게 상호작용 중임을 선언
            UIManagerTemp.instance.installationSetUI.SetActive(true); //유아이 매니저가 관리하고 있는 설치물 관리 UI 사용 선언
        }
        //상호작용중인 객체가 있을때 설정
    }

    public virtual void OnColliderInteract()
    {
        Debug.Log("대상 오브젝트 인벤토리에");
    }
}
