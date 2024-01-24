using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chocolate : IngredientData
{
    public Sprite chocolateIcon;
    public override void InitSetting()
    {
        stat.name = "Chocolate";
        stat.discription = "This is chocolate topping";
        stat.icon = chocolateIcon;

        stat.moveSpeed = 2f;

        stat.VisitGameObjects = new List<GameObject>();

        stat.canStack = false;
        stat.maxStackAmount = 1;
    }

    public override bool Continuous()
    {
        return false;
    }
}
