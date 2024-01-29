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
    public GameObject curGameObject;
    public bool isClick;

    private void Update()
    {
        if (isClick)
        {
            //현재 상호작용중인 객체 업데이트
            if(!GameManager.instance.installationManager.curInstallation)
                return;
            
            curGameObject = GameManager.instance.installationManager.curInstallation;
            
            Vector2 curMouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            curGameObject.transform.position = tilemap.WorldToCell(curMouseDirection);
            curGameObject.transform.position = new Vector2(curGameObject.transform.position.x + 0.5f, curGameObject.transform.position.y + 1.5f);
            transform.root.position = new Vector2(curMouseDirection.x, curMouseDirection.y + (transform.root.position.y - gameObject.transform.position.y));
        }
        else if(curGameObject)
        {
            //놓았을 때 타일맵에 맞게 UI 정리
            transform.root.position = curGameObject.transform.position;
            GameManager.instance.dataManager.PosUpdate(curGameObject);
            //움직일 객체 초기화
            curGameObject = null;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isClick = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isClick = false;
    }
}
