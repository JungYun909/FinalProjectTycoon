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

        stat.haveInventory = false;
    }

    public override bool Continuous()
    {
        return false;
    }
}
