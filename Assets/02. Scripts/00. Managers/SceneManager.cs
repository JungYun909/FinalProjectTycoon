using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType     
{
    TitleScene,
    MainScene,
    Kitchen,
    EndScene,
    HappyEndScene,
}
public class SceneManager : MonoBehaviour    // TODO 씬 변경. 씬 로드시 계속 유지는 필요함. 씬 변경시 다른 매니저들이 일핡 수 있도록 이벤트 정도 발생?
{
    public void Initialize()
    {
        string curScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (sceneNameToType.TryGetValue(curScene, out SceneType type))
        {
            sceneInfo?.Invoke(type);
            Debug.Log($"Scene info handled: {curScene}");
        }
    }
    private Dictionary<string, SceneType> sceneNameToType = new Dictionary<string, SceneType>()
    {
        { "TitleScene", SceneType.TitleScene },
        { "MainScene", SceneType.MainScene },
        { "Kitchen" , SceneType.Kitchen },
        { "EndScene" , SceneType.EndScene},
        { "HappyEndScene" , SceneType.HappyEndScene}
    };
    public event Action<SceneType> sceneInfo;
    public void ChangeScene(string sceneName)
    {
        if (sceneNameToType.TryGetValue(sceneName, out SceneType type))
        {
            try
            {
                StartCoroutine(LoadSceneAsync(sceneName, type));
            }
            catch (Exception e)
            {
                Debug.Log("SceneError");
                throw;
            }
        }
        else
        {
            Debug.Log("Invalid scene name");
        }
    }
    
    private IEnumerator LoadSceneAsync(string sceneName, SceneType type)
    {
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        OnSceneChange(type);
    }

    public void OnSceneChange(SceneType type)
    {
        sceneInfo?.Invoke(type);
    }
}

