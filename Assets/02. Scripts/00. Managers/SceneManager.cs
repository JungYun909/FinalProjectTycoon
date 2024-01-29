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
    private SceneType _type;
    public void ChangeScene(string sceneName)
    {
        try
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
        catch (Exception e)
        {
            Debug.Log("SceneError");
            throw;
        }
    }

    
}
