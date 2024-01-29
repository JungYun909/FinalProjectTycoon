using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

// public abstract class Manager : MonoBehaviour
// {
//     public void Start()
//     {
//         throw new NotImplementedException();
//     }
// }

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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }
}
