using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public interface IInteractable
{
    bool Continuous();
    void OnInteract();
}
public class InteractionManager : MonoBehaviour
{
    public static InteractionManager instance;

    public GameObject curGameObject;
    public GameObject targetGameObject;

    private IInteractable interactionObject;
    private Vector2 curMouseDirection;
    private Coroutine interactionCoroutine;
    private bool IsClick;

    private void Awake()
    {
        instance = this;
    }

    public void OnLook(InputValue value)
    {
        curMouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnClick(InputValue value)
    {
        RaycastHit2D ray = Physics2D.Raycast(curMouseDirection, Vector2.zero, 0f);
        
        bool isCurClick = false;
        
        if(!ray.collider)
            return;

        interactionObject = ray.collider.gameObject.GetComponent<IInteractable>();
        
        if(interactionObject == null)
            return;
        
        if (value.isPressed && interactionObject.Continuous())
        {
            // 코루틴 시작
            interactionCoroutine = StartCoroutine(InteractionCoroutine());
        }
        else if(!value.isPressed && interactionObject.Continuous())
        {
            // 마우스 클릭이 끝날 때 코루틴 중지
            if (interactionCoroutine != null)
            {
                StopCoroutine(interactionCoroutine);
                
            }
            return;
        }
        
        if (value.isPressed && isCurClick == false)
        {
            isCurClick = true;
            if (!curGameObject)
            {
                curGameObject = ray.collider.gameObject;
                interactionObject.OnInteract();
            }
            else
            {
                interactionObject.OnInteract();
                curGameObject.GetComponent<IInteractable>().OnInteract();
            }
        }
        else if (!value.isPressed && isCurClick)
        {
            isCurClick = false;
        }
    }
    
    IEnumerator InteractionCoroutine()
    {
        // 마우스 클릭이 끝날 때까지 반복
        while (true)
        {
            interactionObject.OnInteract();

            // 한 프레임 대기
            yield return null;
        }
    }
}
