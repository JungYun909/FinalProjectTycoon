using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorUI : UIBase
{

    public Button button;
    private void OnEnable()
    {
        button.onClick.AddListener(CloseThisWindow);
    }
    public override void Initialize()
    {
        
    }

    public override void UpdateUI()
    {
        
    }

    public void CloseThisWindow()
    {
        GameManager.instance.uiManager.GoBack();
    }
}
