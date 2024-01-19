using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MachineButtonController : MonoBehaviour
{
    private InstallObject installObject;
    public GameObject prefab;//TODO
    public List<GameObject> prefabs;

    private void Start()
    {
        installObject = GetComponent<InstallObject>();
    }
    public void OnDeleteBtn()
    {
        installObject.Delete();
    }

    public void OnInstallBtn()
    {
        installObject.Install(prefab, gameObject);
    }
}
