using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dough : IngredientData
{
    public Sprite doughIcon;
    public override void InitSetting()
    {
        stat.name = "Dough";
        stat.discription = "This is the most basic ingredient";
        stat.icon = doughIcon;

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
