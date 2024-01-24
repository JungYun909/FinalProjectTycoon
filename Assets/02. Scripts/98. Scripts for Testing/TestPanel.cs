using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPanel : UIBase
{
    [SerializeField] UIBase uitoOpen;


    public override void Initialize()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateUI()
    {
        throw new System.NotImplementedException();
    }
   
    public void OpenPanel()
    {
        uiManager.OpenWindow(uitoOpen);
    }
}
