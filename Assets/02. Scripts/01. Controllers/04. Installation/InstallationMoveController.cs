using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
public class InstallationMoveController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Tilemap tilemap;
    public InstallationSetController controller;
    private Coroutine movementCoroutine;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(controller.curGameObject)
            movementCoroutine = StartCoroutine(StartMovement());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (controller.curGameObject)
        {
            StopCoroutine(movementCoroutine);
            transform.root.position = controller.curGameObject.transform.position;
            GameManager.instance.dataManager.PosUpdate(controller.curGameObject);
        }
    }

    IEnumerator StartMovement()
    {
        while (true)
        {
            Vector2 curMouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            controller.curGameObject.transform.position = tilemap.WorldToCell(curMouseDirection);
            controller.curGameObject.transform.position = new Vector2(controller.curGameObject.transform.position.x + 0.5f, controller.curGameObject.transform.position.y + 1.5f);
            transform.root.position = new Vector2(curMouseDirection.x, curMouseDirection.y + (transform.root.position.y - gameObject.transform.position.y));

            yield return null;
        }

        //움직일 객체 초기화
    }
}
