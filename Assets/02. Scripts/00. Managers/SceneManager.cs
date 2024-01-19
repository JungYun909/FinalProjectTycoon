using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType     
{
    MainScene,
    Kitchen
}
public class SceneManager : MonoBehaviour    // 씬 변경. 씬 로드시 계속 유지는 필요함
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
