using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType     
{
    TitleScene,
    MainScene,
    Kitchen
}
public class SceneManager : MonoBehaviour    // TODO 씬 변경. 씬 로드시 계속 유지는 필요함. 씬 변경시 다른 매니저들이 일핡 수 있도록 이벤트 정도 발생?
{
    private Dictionary<string, SceneType> sceneNameToType = new Dictionary<string, SceneType>()
    {
        { "TitleScene", SceneType.TitleScene },
        { "MainScene", SceneType.MainScene },
        { "Kitchen", SceneType.Kitchen }
    };
    public delegate void HandleSceneInfo(SceneType type);
    public static event HandleSceneInfo sceneInfo;
    public void ChangeScene(string sceneName)
    {
        if (sceneNameToType.TryGetValue(sceneName, out SceneType type))
        {
            try
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
                OnSceneChange(type);
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

    public void OnSceneChange(SceneType type)
    {
        sceneInfo?.Invoke(type);
    }
}

