using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void OpenUI(UIBase openUI)
    {
        GameManager.instance.uiManager.OpenWindow(openUI);
    }
}
