using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class MachineController : MonoBehaviour
{
    private InstallObject installObject;
    [SerializeField] private Tilemap tilemap;
    public GameObject prefab;

    private void Awake()
    {
        installObject = GetComponent<InstallObject>();
    }
    void Start()
    {
        installObject.InitSetting();
    }

    public void OnDeleteBtn()
    {
        installObject.Delete();
    }

    public void OnInstallBtn()
    {
        installObject.Install(prefab, gameObject);
    }

    public void OnClickMachine()
    {
        SpriteRenderer curSpriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        if(UIManagerTemp.instance.canvas.gameObject.activeSelf)
        {
            UIManagerTemp.instance.canvas.gameObject.transform.position = gameObject.transform.position;
            UIManagerTemp.instance.UIImageChange(curSpriteRenderer.sprite);
        }
    }

    
}
