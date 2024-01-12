using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
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
