using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBtn : UIData
{
    public GameObject curPrefab;
    
    public override void InitSetting()
    {
        _uiStat.name = "Move Button";
        _uiStat.type = UIType.Btn;
        _uiStat.Prefab = curPrefab;
    }

    public override void OnInteract()
    {
        curPrefab.transform.position = mouseDirection;
    }
}
