using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDestinationSet : MonoBehaviour
{
    public MovementController movementController;
    public NPCController controller;

    public Coroutine curCoroutine;

    public void MachinePosInform()
    {
        float distanceToMachine = 0; 
        
        controller.destinationObj = null;
        
        Vector2 curObjPos = new Vector2(transform.position.x, transform.position.y);
        movementController.speed = controller.curNPCData.speed;
        

        if (controller.visitCounter == true)
        {
            movementController.destinationObj = GameManager.instance.dataManager.entrance;
            return;
        }

        if (controller.visitObj.Count == 0)
        {
            movementController.destinationObj = GameManager.instance.dataManager.counter;
            controller.visitCounter = true;
            return;
        }

        foreach (var standMachine in controller.visitObj)
        {
            if (distanceToMachine < Vector2.Distance(standMachine.transform.position, curObjPos))
            {
                controller.destinationObj = standMachine;
                distanceToMachine = Vector2.Distance(standMachine.transform.position, curObjPos);
            }
        }

        if (controller.destinationObj != null)
        {
            movementController.destinationObj = controller.destinationObj;
        }
    }

    public void StartCoroutine()
    {
        StartCoroutine(StartPosInform());
    }

    private IEnumerator StartPosInform()
    {
        yield return new WaitForSeconds(1f);
        MachinePosInform();
    }
}
