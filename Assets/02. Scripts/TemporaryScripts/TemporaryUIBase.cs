using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryUIBase : UIBase
{
    public override void Initialize()
    {
        Debug.Log("Initialized");
    }

    public override void UpdateUI()
    {
        Debug.Log("Updated");
    }

}
