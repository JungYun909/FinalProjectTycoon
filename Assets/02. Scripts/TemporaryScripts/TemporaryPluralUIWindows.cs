using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryPluralUIWindows : UIBase
{
    public override void Initialize()
    {
        Debug.Log("BB");
    }

    public override void UpdateUI()
    {
        Debug.Log("AA");
    }

    public UIBase firstWindow;
    public UIBase secondWindow;
    public UIBase thirdWindow;
    public UIBase fourthWindow;

    public UIManager uiManager;


    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    public void OpenFirstUIWindow()
    {
        uiManager.OpenWindow(firstWindow);
    }

    public void OpenSecondUIWindow()
    {
        uiManager.OpenWindow(secondWindow);
    }

    public void OpenThirdWindow()
    {
        uiManager.OpenWindow(thirdWindow);
    }

    public void OpenFourthWindow()
    {
        uiManager.OpenWindow(fourthWindow);
    }

    public void Back()
    {
        uiManager.GoBack();
    }

    public void CloseAllWindow()
    {
        uiManager.CloseAll();
    }
}
