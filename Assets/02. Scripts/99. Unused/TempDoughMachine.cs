//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;

//public class DoughMachine : MonoBehaviour
//{
//    public GameObject dough;
//    public GameObject destination;
    
//    private MovementController _movementController;

//    private void Start()
//    {
//        InvokeRepeating("Spawn", 0, 3);
//    }

//    private void Spawn()
//    {
//        GameObject obj = PoolManager.instacne.SpawnFromPool(dough);
//        obj.transform.position = gameObject.transform.position;
        
//        if (obj)
//        {
//            obj.GetComponent<ItemObject>().item.InitSetting();
//            obj.GetComponent<MovementController>().Move(destination);
//        }

//    }
//}
