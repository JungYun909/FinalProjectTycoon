using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallationBtnController : MonoBehaviour
{
    
    public void OnDeleteBtn()
    {
        UIManagerTemp.instance.installationSetUI.SetActive(false);
        Destroy(InteractionManager.instance.curGameObject);
        InteractionManager.instance.curGameObject = null;
        InteractionManager.instance.targetGameObject = null;
        //인벤토리에 추가

    }

    public void OnInstallBtn()
    {
        UIManagerTemp.instance.installationSetUI.SetActive(true);
        UIManagerTemp.instance.installUI.SetActive(false);
    }
    
    public void DestinationSetBtn()
    {
        UIManagerTemp.instance.installationSetUI.SetActive(false);
    }

    public void InstallationSetBtn(GameObject InstallationSetUI)
    {
        UIManagerTemp.instance.installationSetUI.SetActive(false);
        UIManagerTemp.instance.installUI.SetActive(true);
        UIManagerTemp.instance.installUI.transform.position = InteractionManager.instance.curGameObject.transform.position;

    }

    public void BackBtn()
    {
        UIManagerTemp.instance.installationSetUI.SetActive(false);
        InteractionManager.instance.curGameObject = null;
        InteractionManager.instance.targetGameObject = null;
    }
}
