using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBController : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.fbManager.InitSet();
    }
}
