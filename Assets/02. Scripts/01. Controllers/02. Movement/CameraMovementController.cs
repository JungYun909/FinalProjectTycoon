using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class CameraMovementController : MonoBehaviour
{
    public Camera main;

    private bool isMain = true;

    public void CameraMoveToggle()
    {
        if (isMain)
        {
            main.transform.position = new Vector3(main.transform.position.x, main.transform.position.y - 11, main.transform.position.z);
            isMain = false;
            Debug.Log(isMain);
        }
        else
        {
            main.transform.position = new Vector3(main.transform.position.x, main.transform.position.y + 11, main.transform.position.z);
            isMain = true;
            Debug.Log(isMain);
        }
    }
}
