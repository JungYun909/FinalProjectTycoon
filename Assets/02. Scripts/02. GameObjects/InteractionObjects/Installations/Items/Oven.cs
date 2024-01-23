using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : InstallationData
{
    public Inventory curInventory;
    public override void InitSetting()
    {
        stat.name = "Oven";
        stat.discription = "Cook until golden brown";

        stat.destinationInstallation = null;

        stat.canSpawn = true;
        stat.spawnPrefab = null;
        stat.spawnDelay = 10f;

        stat.haveInventory = true;
        stat.curInventoryItem = null;
        stat.inventory = curInventory;
        stat.installationInventory = new Queue<GameObject>();

        stat.haveMinigame = true;
        stat.curGauge = 100f;
        stat.decreaseTime = 1f;
    }

    public override bool Continuous()
    {
        return false;
    }
}
