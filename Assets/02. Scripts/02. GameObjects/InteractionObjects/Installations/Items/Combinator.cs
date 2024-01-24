using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combinator : InstallationData
{
    public Inventory curInventory;
    public IngredientInventory curIngredientInventory;
    public override void InitSetting()
    {
        stat.name = "Combinator";
        stat.discription = "Combine the ingredients";

        stat.destinationInstallation = null;

        stat.canSpawn = true;
        stat.spawnPrefab = null;
        stat.spawnDelay = 10f;

        stat.haveInventory = true;
        stat.curInventoryItem = null;
        stat.inventory = curInventory;
        stat.installationInventory = new Queue<GameObject>();

        stat.haveIngredientInventory = true;
        stat.curIngredientInventoryItem = null;
        stat.ingredientInventory = curIngredientInventory;
        stat.installationIngredientInventory = new Queue<GameObject>();

        stat.haveMinigame = false;
        stat.curGauge = 100f;
        stat.decreaseTime = 0f;
    }

    public override bool Continuous()
    {
        return false;
    }
}
