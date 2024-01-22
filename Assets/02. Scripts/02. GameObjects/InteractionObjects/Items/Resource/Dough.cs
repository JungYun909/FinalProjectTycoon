using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Dough : InteractionData
{
    public Sprite curIcon;
    public override void InitSetting()
    {
        _interactionStat.name = "Dough";
        _interactionStat.description = "Basic ingredients for bread";
        _interactionStat.type = InteractionType.Ingredient;
        _interactionStat.icon = curIcon;
        _interactionStat.canCell = false;
        _interactionStat.price = 0;
        _interactionStat.canMove = true;
        _interactionStat.speed = 0.005f;
    }

    public override void OnInteract()
    {
        PoolManager.instacne.DeSpawnFromPool(gameObject);
    }
}
