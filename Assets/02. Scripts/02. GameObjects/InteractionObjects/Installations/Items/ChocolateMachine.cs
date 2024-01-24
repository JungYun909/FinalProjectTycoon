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

        stat.destinationInstallation = null;

        stat.canSpawn = true;
        stat.spawnPrefab = curSpawnPrefab;
        stat.spawnDelay = 3f;

        stat.haveInventory = false;
        stat.curInventoryItem = null;
        stat.inventory = null;
        stat.installationInventory = null;

        stat.haveIngredientInventory = false;
        stat.curIngredientInventoryItem = null;
        stat.ingredientInventory = null;
        stat.installationIngredientInventory = null;

        stat.haveMinigame = false;
        stat.curGauge = 0f;
        stat.decreaseTime = 0f;
    }

    public override bool Continuous()
    {
        return false;
    }
}
