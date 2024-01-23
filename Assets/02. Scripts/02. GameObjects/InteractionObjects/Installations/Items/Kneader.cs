using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kneader : InstallationData
{
    public GameObject curSpawnPrefab;
    public override void InitSetting()
    {
        stat.name = "Kneader";
        stat.discription = "Produces dough";

        stat.canSpawn = true;
        stat.spawnPrefab = curSpawnPrefab;
        stat.spawnDelay = 3f;
        stat.moveSpeed = 0.05f;
    }

    public override bool Continuous()
    {
        return false;
    }
}
