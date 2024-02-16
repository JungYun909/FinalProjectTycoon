using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class InstallationDestinationController : MonoBehaviour
{
    public GameObject[] destination = new GameObject[2];
    public LineRenderer line;
    public GameObject lineObj;
    public int destinationID;

    private Coroutine destinationCoroutine;

    private Vector2 desPos0;
    public Vector2 desPos1;

    public InstallationController controller;

    public event Action<GameObject, GameObject> OnDestinationEvent;

    public void InitSet()
    {
        controller.installationFuctionSet += destinationFunction;
        controller.installationFuctionOut += stopFunction;

        destinationFunction();
    }

    private void destinationFunction()
    {
        if (destinationCoroutine == null)
            destinationCoroutine = StartCoroutine(StartDestinationSet());
    }

    private void stopFunction()
    {
        if (destinationCoroutine != null)
        {
            StopCoroutine(destinationCoroutine);
            destinationCoroutine = null;
            lineObj.SetActive(false);
        }
        if(destination[0]!=null && destination[1] != null)
        {
            GameManager.instance.destinationManager.RegisterDestinationInfo(destination[0].GetComponent<InstallationController>().destinationID, destination[1].GetComponent<InstallationController>().destinationID);
            this.gameObject.GetComponentInParent<InstallationController>().SaveDestination();
        }
        else if (destination[0] != null)
        {
            GameManager.instance.destinationManager.DeleteDestinationInfo(destination[0].GetComponentInParent<InstallationController>().destinationID);
        }
    }

    IEnumerator StartDestinationSet()
    {
        lineObj.SetActive(true);

        while (true)
        {
            RaycastHit2D ray = Physics2D.Raycast(GameManager.instance.interactionManager.curMouseDirection, Vector2.zero, 0f, LayerMask.GetMask("Installation"));
        
            if (ray.collider)
            {
                if (destination[0] == null)
                {
                    destination[0] = ray.collider.gameObject;
                    desPos0 = destination[0].transform.position;
                }
                else if (destination[0] != ray.collider.gameObject)
                {
                    destination[1] = ray.collider.gameObject;
                    desPos1 = destination[1].transform.position;
                    OnDestinationEvent?.Invoke(destination[0], destination[1]);
                }
            }
            else
            {
                destination[1] = null;
            }

            if (!destination[0])
            {
                line.SetPosition(0, Vector2.zero);
                line.SetPosition(1, Vector2.zero);
            }
            else if (destination[0])
            {
                if (!ray.collider || !destination[1])
                    line.SetPosition(1, GameManager.instance.interactionManager.curMouseDirection - desPos0);
                else
                    line.SetPosition(1, desPos1 - desPos0);
            }
            yield return null;
        }
    }
}
