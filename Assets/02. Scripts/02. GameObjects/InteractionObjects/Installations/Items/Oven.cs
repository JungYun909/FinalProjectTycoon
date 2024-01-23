using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : InstallationData
{
    public override void InitSetting()
    {
        stat.name = "Oven";
        stat.discription = "Cook until golden brown";

        stat.canSpawn = true;
        stat.spawnDelay = 10f;

        stat.haveInventory = true;
        stat.installationInventory = new Queue<GameObject>();
    }

    public override bool Continuous()
    {
        return false;
    }
}
