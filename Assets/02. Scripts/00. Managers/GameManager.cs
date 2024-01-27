using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Manager : MonoBehaviour
{
    public void Start()
    {
        throw new NotImplementedException();
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
    public SceneManager sceneManager;


    //private PlayerInputManager playerInputManager;

    private void Awake()
    {
        //audioManager = GetComponent<AudioManager>();
        //itemManager = GetComponent<ItemManager>();
        //logicManager = GetComponent<LogicManager>();
        //timeManager = GetComponent<TimeManager>();

        //spawnManager = GetComponent<SpawnManager>();
        //installationManager = GetComponent<InstallationManager>();
        //inventoryManager = GetComponent<InventoryManager>();
        //poolManager = GetComponent<PoolManager>();
        //uiManager = GetComponent<UIManager>();
        //interactionManager = GetComponent<InteractionManager>();
        //statManager = GetComponent<StatManager>();
        //dataManager = GetComponent<DataManager>();
        //sceneManager = GetComponent<SceneManager>();
    }

    private void Start()
    {
        instance = this;
        //if(instance = null)
        //{
        //    instance = this;
        //}
        //else if(instance != this)
        //{
        //    Destroy(gameObject);
        //}
        //DontDestroyOnLoad(gameObject);
    }
}
