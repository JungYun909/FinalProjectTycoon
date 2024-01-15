using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Foods : MovementObject
{
    public override void InitSetting()
    {
        moveData.speed = 0.002f;
    }

    public override void Spawn(GameObject food, GameObject startGameObject)
    {
        food = gameObject;
        base.Spawn(food, startGameObject);
    }

    public override void DeSpawn(GameObject food, GameObject endGameObject)
    {
        food = gameObject;
        base.DeSpawn(food, endGameObject);
    }
}
