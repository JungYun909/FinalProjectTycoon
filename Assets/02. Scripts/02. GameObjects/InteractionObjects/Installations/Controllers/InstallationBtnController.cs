using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallationBtnController : MonoBehaviour
{
    public GameObject installationGameObject;
    public GameObject installationUI;
    
    public void OnDeleteBtn()
    {
        transform.root.gameObject.SetActive(false);
        Destroy(installationGameObject);
        //인벤토리에 추가

    }

    public void OnInstallBtn()
    {
        transform.root.gameObject.SetActive(false);
        installationUI.SetActive(true);
    }
    
    public void DestinationSetBtn()
    {
        transform.root.gameObject.SetActive(false);
    }

    public void InstallationSetBtn(GameObject InstallationSetUI)
    {
        transform.root.gameObject.SetActive(false);
        InstallationSetUI.SetActive(true);
    }

    public void BackBtn()
    {
        transform.root.gameObject.SetActive(false);
        InteractionManager.instance.curGameObject = null;
        InteractionManager.instance.targetGameObject = null;
    }
}
