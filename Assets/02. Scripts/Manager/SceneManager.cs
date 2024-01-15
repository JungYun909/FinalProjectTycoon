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
public class SceneManager : MonoBehaviour
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
