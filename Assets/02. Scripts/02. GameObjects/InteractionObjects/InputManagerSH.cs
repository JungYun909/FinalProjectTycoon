using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerSH : MonoBehaviour
{
    public static InputManagerSH instance;

    private InteractionObject interactionObject;
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

        interactionObject = ray.collider.gameObject.GetComponent<InteractionObject>();

        if (value.isPressed && interactionObject._interactionData._interactionStat.isClick)
        {
            // 코루틴 시작
            interactionCoroutine = StartCoroutine(InteractionCoroutine());
        }
        else if(!value.isPressed && interactionObject._interactionData._interactionStat.isClick)
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
            interactionObject._interactionData.OnInteract();
            isCurClick = true;
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
            interactionObject._interactionData.OnInteract();

            // 한 프레임 대기
            yield return null;
        }
    }
}
