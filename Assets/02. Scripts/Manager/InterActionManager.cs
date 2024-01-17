using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void OnInteract();
}
public class InteractionManager : MonoBehaviour
{
    public event Action collisionEnter; //TODO 부딧혔을때 받아야할 정보 수정
    public event Action click; 
    public LayerMask targetLayer;
    
    [Header("InteractionOBJ")]
    private GameObject curInteractGameobject;
    private IInteractable curInteractable;


    private void OnEnable()
    {
        InputManager.OnClicked += WorkOnClick;
    }
    //상호작용 상속받아 사용 가능한 오브젝트의 기능을 실행하고 초기화

    private void OnDisable()
    {
        InputManager.OnClicked -= WorkOnClick;
    }

    private void InteractionWithItem()
    {
        if (curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractGameobject = null;
            curInteractable = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        curInteractGameobject = other.collider.gameObject;
        curInteractable = other.collider.gameObject.GetComponent<IInteractable>(); 
    }

    public void WorkOnClick(Vector2 worldPosition)
    {
        Debug.Log($"WorkOnClick in InteractionManager called with position: {worldPosition}");

        // 레이캐스트 방향 및 거리 설정
        Vector2 rayDirection = Vector2.down; // 예시로 아래 방향을 설정
        float rayLength = 10f; // 레이캐스트 길이 설정

        // 레이캐스트 디버깅
        Debug.DrawRay(worldPosition, rayDirection * rayLength, Color.red, 2f);

        // 레이캐스트 수행
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, rayDirection, rayLength, targetLayer);

        if (hit.collider != null)
        {
            Debug.Log($"Hit object: {hit.collider.gameObject.name}");
            curInteractable = hit.collider.gameObject.GetComponent<IInteractable>();

            if (curInteractable != null)
            {
                Debug.Log("Found IInteractable component, calling OnInteract");
                curInteractable.OnInteract();
            }
            else
            {
                Debug.Log("No IInteractable component found on hit object");
            }
        }
        else
        {
            Debug.Log("No object hit by Raycast");
        }
    }
}