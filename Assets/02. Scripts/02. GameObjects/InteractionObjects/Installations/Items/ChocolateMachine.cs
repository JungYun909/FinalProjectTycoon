using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocolateMachine : InstallationData
{
    public GameObject curSpawnPrefab;
    public override void InitSetting()
    {
        stat.name = "ChocolateMachine";
        stat.discription = "Produces chocolate";

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
