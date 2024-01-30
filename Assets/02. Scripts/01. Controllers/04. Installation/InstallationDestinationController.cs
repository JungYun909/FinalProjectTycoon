using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallationDestinationController : MonoBehaviour
{
    public GameObject[] destination = new GameObject[2];
    public LineRenderer line;
    public GameObject lineObj;
    
    private Coroutine destinationCoroutine;
    
    private Vector2 desPos0;
    private Vector2 desPos1;
    
    public InstallationController controller;
    
    private void Start()
    {
        controller.installationFuctionSet += destinationFunction;
        controller.installationFuctionOut += stopFunction;
        
        destinationFunction();
    }

    private void destinationFunction()
    {
        destinationCoroutine = StartCoroutine(StartDestinationSet());
    }

    private void stopFunction()
    {
        StopCoroutine(destinationCoroutine);
        lineObj.SetActive(false);
    }
    
    IEnumerator StartDestinationSet()
    {
        while (true)
        {
            lineObj.SetActive(true);
        
            RaycastHit2D ray = Physics2D.Raycast(GameManager.instance.interactionManager.curMouseDirection, Vector2.zero, 0f);
        
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
                    line.SetPosition(1, GameManager.instance.interactionManager.curMouseDirection - desPos0);
                else
                    line.SetPosition(1, desPos1 - desPos0);
            }

            yield return null;
        }
    }
}