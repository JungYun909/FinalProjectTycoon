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
    public virtual void Install(GameObject spawnObj, GameObject gameObject1)
    {
        //Instantiate(data.curGameObject);
        Instantiate(spawnObj, gameObject.transform.position, Quaternion.identity);
    }

    public virtual void Delete()
    {
        UIManagerTemp.instance.canvas.gameObject.SetActive(false);
    }

    public virtual void Move(Vector2 direction)
    {
        data.curGameObject.transform.position = direction;
    }

   
}
