using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallationInventoryController : MonoBehaviour,IInteractable
{
    public bool Continuous()
    {
        return false;
    }

    public void OnClickInteract()
    {
        GameObject curInstallationSetObj = GameManager.instance.installationManager.curInstallation;

        if (!curInstallationSetObj)
        {
            GameManager.instance.installationManager.curInstallation = gameObject;
            GameManager.instance.installationManager.OnInstallationSetUI();
        }
        else if (curInstallationSetObj == gameObject)
        {
            GameManager.instance.installationManager.OnInstallationSetUI();
        }
        else
        {
            InstallationController controller = GameManager.instance.installationManager.curInstallation.GetComponent<InstallationController>();
            // controller.destinationObj = gameObject;
            GameManager.instance.installationManager.OnInstallationSetUI();
        }
    }

    public void OffClickInteract()
    {
        return;
    }

    public void OnColliderInteract()
    {
        //온콜라이더 상호작용 내용
    }
}
