using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : InstallObject
{
    public override void InitSetting()
    {
        data.curGameObject = gameObject;
    }
}
