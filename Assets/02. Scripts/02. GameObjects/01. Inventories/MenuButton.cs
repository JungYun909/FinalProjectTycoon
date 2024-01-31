using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void OpenUI(UIBase openUI)
    {
        GameManager.instance.uiManager.OpenWindow(openUI);
    }

    public void InstallationFunctionIndexSet(int index)
    {
        GameManager.instance.interactionManager.installationFunctionIndex = index;
    }

    public void BackBtn()
    {
        GameManager.instance.uiManager.CloseAll();
    }
}
