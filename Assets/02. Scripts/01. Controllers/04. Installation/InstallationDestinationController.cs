using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallationDestinationController : MonoBehaviour, IInteractable
{
    public GameObject[] destination = new GameObject[2];
    public LineRenderer line;
    public GameObject lineObj;
    
    private Vector2 desPos0;
    private Vector2 desPos1;

    public bool Continuous()
    {
        return true;
    }

    public void OnClickInteract()
    {
        lineObj.SetActive(true);
        
        RaycastHit2D ray = Physics2D.Raycast(InteractionManager.instance.curMouseDirection, Vector2.zero, 0f);
        
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
        else
        {
            destination[1] = null;
        }

        if (destination[0])
        {
            line.SetPosition(0,Vector2.zero);
            
            if(!ray.collider)
                line.SetPosition(1, InteractionManager.instance.curMouseDirection - desPos0);
            else
                line.SetPosition(1, desPos1 - desPos0);
        }
    }

    public void OffClickInteract()
    {
        lineObj.SetActive(false);
    }

    public void OnColliderInteract()
    {
        return;
    }
}
