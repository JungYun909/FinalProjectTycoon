using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

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
    private InterActionManager interactionManager;
    private ItemManager itemManager;
    private LogicManager logicManager;
    private PoolManager poolManager;
    private TimeManager timeManager;
    private UIManager uiManager;
    private SceneHandler sceneHandler;
    private StatHandler statHandler;
    private PlayerInputController playerInputController;

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
