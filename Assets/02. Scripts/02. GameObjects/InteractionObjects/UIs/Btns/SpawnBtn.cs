using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnBtn : InteractionData
{
    public GameObject spawnGameObject;
    public override void InitSetting()
    {
        _interactionStat.name = "Move Button";
        _interactionStat.type = InteractionType.UI;
        _interactionStat.isClick = false;
    }

    public override void OnInteract()
    {
        Instantiate(spawnGameObject, Vector2.zero, Quaternion.identity);
    }
}
