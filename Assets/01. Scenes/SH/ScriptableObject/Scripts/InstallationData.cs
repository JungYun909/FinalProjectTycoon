using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Installation Data", menuName = "InstallationSO", order = 0)]
public class InstallationData : ScriptableObject
{
    [Header("Info")] 
    public string name;
    public string discription;
    public Sprite sprite;

    [Header("Destination")]
    public GameObject destinationInstallation;

    [Header("Spawning")]
    public bool canSpawn;
    public float spawnDelay;
    public IngredientData spawnData;

    [Header("Inventory")]
    public bool haveDoughInventory;
    public bool haveIngredientInventory;
    public float makeDelay;
    public Queue<GameObject> doughContainer;
}
