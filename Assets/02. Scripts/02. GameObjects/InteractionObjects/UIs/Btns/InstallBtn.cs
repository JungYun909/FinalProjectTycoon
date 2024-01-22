using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class InstallBtn : InteractionData
{
    public override void InitSetting()
    {
        _interactionStat.name = "Install Button";
        _interactionStat.type = InteractionType.UI;
    }

    public override void OnInteract()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
