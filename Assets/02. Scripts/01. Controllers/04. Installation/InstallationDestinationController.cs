using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallationDestinationController : MonoBehaviour, IInteractable
{
    public GameObject[] destination = new GameObject[2];
    public LineRenderer line;

    public bool Continuous()
    {
        return true;
    }

    public void OnClickInteract()
    {
        RaycastHit2D ray = Physics2D.Raycast(InteractionManager.instance.curMouseDirection, Vector2.zero, 0f);

        Vector2 desPos0 = new Vector2();
        Vector2 desPos1 = new Vector2();
        
        if (ray.collider)
        {
            if (destination[0] == null)
            {
                destination[0] = ray.collider.gameObject;
                desPos0 = destination[0].transform.position;
            }
            else if(destination[0] != ray.collider.gameObject)
            {
                destination[1] = ray.collider.gameObject;
                desPos1 = destination[1].transform.position;
            }
        }

        if(!destination[0])
            line.SetPosition(0,Vector2.zero);

        if (!destination[1])
        {
            if(!ray.collider)
                line.SetPosition(1, InteractionManager.instance.curMouseDirection - desPos0);
            else
                line.SetPosition(1, desPos1 - desPos0);

        }
    }

    public void OnColliderInteract()
    {
        return;
    }
}
