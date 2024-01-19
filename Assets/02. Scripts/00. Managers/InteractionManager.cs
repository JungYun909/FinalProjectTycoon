using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void OnInteract();
}

public class InteractionManager : MonoBehaviour    	// TODO 정확히 무슨 일을 관리할 것인가? 상호작용이 무엇? 어떤 오브젝트를 클릭했는지를 판별?
                                                    // TODO 발생 시점을 관리하도록 함?
                                                    // Function : 어떤 것을 OnInteract할 지 판별해주기 위한 매니저
                                                    // 클릭했을 때나 부딪혔을 때의 OnInteract 분리 가능하도록?
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

    private void OnCollisionEnter2D(Collision2D other)   // MonoBehaviour가 아닐 떄?
    {
        curInteractGameobject = other.collider.gameObject;
        curInteractable = other.collider.gameObject.GetComponent<IInteractable>(); 
    }

    public void WorkOnClick(Vector2 worldPosition)    // TODO 아래 객체의 LayerMask가 무엇인지 판단하도	?
    {
        Debug.Log($"WorkOnClick in InteractionManager called with position: {worldPosition}");

        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.down, Mathf.Infinity);

        if (hit.collider != null)
        {
            curInteractable = hit.collider.gameObject.GetComponent<IInteractable>();

            if (curInteractable != null)
            {
                Debug.Log("Found IInteractable component, calling OnInteract");
                curInteractable.OnInteract();
            }
            else
            {
                Debug.Log("Hit object is not IInteractable");
            }
        }
        else
        {
            Debug.Log("No object hit by Raycast");
        }
    }
}