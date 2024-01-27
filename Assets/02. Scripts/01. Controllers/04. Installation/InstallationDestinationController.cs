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
            
        if(!ray.collider.gameObject)
            return;

        if (destination[0] == null)
        {
            destination[0] = ray.collider.gameObject;
            line.SetPosition(0,destination[0].transform.position);
        }
        else
        {
            destination[1] = ray.collider.gameObject;
        }

        if (destination[1] == null || Vector2.Distance(InteractionManager.instance.curMouseDirection, new Vector2(destination[1].transform.position.x, destination[1].transform.position.y)) > 1)
        {
            line.SetPosition(1, InteractionManager.instance.curMouseDirection);
        }
        else
        {
            line.SetPosition(1, new Vector2(destination[1].transform.position.x, destination[1].transform.position.y));
        }
    }

    public void OnColliderInteract()
    {
        return;
    }
}
