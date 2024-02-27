using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public SceneManager sceneManager;
    public AudioManager audioManager;
    public ItemManager itemManager;
    public LogicManager logicManager;
    public TimeManager timeManager;
    
    public SpawnManager spawnManager;
    public InstallationManager installationManager;
    public InventoryManager inventoryManager;
    public PoolManager poolManager;
    public UIManager uiManager;
    public InteractionManager interactionManager;
    public StatManager statManager;
    public DataManager dataManager;
    
    public RecipeManager recipeManager;
    public QuestManager questManager;
    public DestinationManager destinationManager;
    public FirebaseAuthManager firebaseAuthManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            uiManager.Initialize();
            sceneManager.Initialize();
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }
}
