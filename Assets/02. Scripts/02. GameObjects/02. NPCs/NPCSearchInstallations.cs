using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSearchInstallations : MonoBehaviour
{
    
    public GameObject counter;
    public List<GameObject> machineObject = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        counter= GameManager.instance.dataManager.counter;
        machineObject = GameManager.instance.dataManager.curInstallations;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
