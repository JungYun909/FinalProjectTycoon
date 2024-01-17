using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MachineController : MonoBehaviour
{
    private InstallObject installObject;
    // Start is called before the first frame update

    private void Awake()
    {
        installObject = GetComponent<InstallObject>();
    }
    void Start()
    {
        installObject.InitSetting();
        //installObject.Delete();
        //installObject = GetComponent<InstallObject>();
    }

    public void OnDeleteBtn()
    {
        installObject.Delete();
    }

    public void OnInstallBtn()
    {
        installObject.Install();
    }

    
}
