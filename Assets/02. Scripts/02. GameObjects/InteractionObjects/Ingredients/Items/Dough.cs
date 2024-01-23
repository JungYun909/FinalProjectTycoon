using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dough : IngredientData
{
    public override void InitSetting()
    {
        stat.name = "Dough";
        stat.discription = "This is the most basic ingredient";

        stat.moveSpeed = 2f;
        stat.VisitGameObjects = new List<GameObject>();
    }

    public override bool Continuous()
    {
        return false;
    }
}
