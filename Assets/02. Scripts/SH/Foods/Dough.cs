using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Dough : ItemData
{
    public Sprite curIcon;
    public override void InitSetting()
    {
        itemStat.name = "Dough";
        itemStat.description = "Basic ingredients for bread";
        itemStat.type = ItemType.Ingredient;
        itemStat.icon = curIcon;
        itemStat.canCell = false;
        itemStat.price = 0;
        itemStat.canMove = true;
        itemStat.speed = 0.005f;
    }
}
