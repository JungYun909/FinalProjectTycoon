using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSample1 : IngredientData
{
    public Sprite NPCsprite;
    public override void InitSetting()
    {
        stat.name = "Cat NPC";
        stat.discription = "Good Smell";
        stat.icon = NPCsprite;

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
