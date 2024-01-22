using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kneader : ItemData
{
    public Sprite curIcon;
    public override void InitSetting()
    {
        itemStat.name = "Kneader";
        itemStat.description = "This is a machine that produces dough";
        itemStat.type = ItemType.Installation;
        itemStat.icon = curIcon;
        itemStat.canCell = true;
        itemStat.price = 100000;
        itemStat.canMove = false;
        itemStat.speed = 0f;
    }

    public override void OnInteract()
    {
        //Installation Set UI Open
    }
}
