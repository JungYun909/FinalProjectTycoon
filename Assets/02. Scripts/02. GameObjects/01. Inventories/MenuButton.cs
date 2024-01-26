using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void OpenUI(UIBase openUI)
    {
        UIManager.Instance.OpenWindow(openUI);
    }
}
