using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct InstallationData
{
    public GameObject curGameObject;
}


public abstract class InstallObject : MonoBehaviour
{
    public InstallationData data;

    public abstract void InitSetting();
    public virtual void Install()
    {
        Instantiate(data.curGameObject);
    }

    public virtual void Delete()
    {
        Destroy(data.curGameObject);
    }

    public virtual void Move(Vector2 direction)
    {
        data.curGameObject.transform.position = direction;
    }

   
}
