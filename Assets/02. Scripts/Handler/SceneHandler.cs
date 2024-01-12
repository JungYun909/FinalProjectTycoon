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
public class SceneHandler : MonoBehaviour
{
    private SceneType _type;
    public void ChangeScene(string sceneName)
    {
        try
        {
            SceneManager.LoadScene(sceneName);
        }
        catch (Exception e)
        {
            Debug.Log("SceneError");
            throw;
        }

    }
}
