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

    private AudioManager audioManager;
    private DataManager dataManager;
    private InteractionManager interactionManager;
    private ItemManager itemManager;
    private LogicManager logicManager;
    private PoolManager poolManager;
    private TimeManager timeManager;
    private UIManager uiManager;
    private SceneManager sceneManager;
    private StatManager statManager;
    private PlayerInputManager playerInputControlleManager;

    // private void Awake()
    // {
    //TODO 매니저들의 경유지로서 역할을 해주자
    // }

    private void Start()
    {
        if(instance = null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
