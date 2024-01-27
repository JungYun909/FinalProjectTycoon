using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = System.Random;

public interface IInteractable
{
    bool Continuous();
    void OnClickInteract();
    void OnColliderInteract();
}
public class InteractionManager : MonoBehaviour
{
    //상호작용에 필요한 변수저장
    public IInteractable interactionObject;
    public Vector2 curMouseDirection;
    private Coroutine interactionCoroutine;
    
    //임시 싱글톤
    public static InteractionManager instance;
    private void Awake()
    {
        instance = this;
    }

    //마우스 포지션 바뀔때마다 정보갱신
    public void OnLook(InputValue value)
    {
        curMouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    //마우스 눌렀을때 상호작용 관리
    public void OnClick(InputValue value)
    {
        
        //클릭 중일떄 발생
        if (value.isPressed && interactionObject == null)
        {
            RaycastHit2D ray = Physics2D.Raycast(curMouseDirection, Vector2.zero, 0f);
            
            if( !ray.collider || ray.collider.gameObject.GetComponent<IInteractable>() == null)
                return;
        
            interactionObject = ray.collider.gameObject.GetComponent<IInteractable>();
            
            if (interactionObject.Continuous()) //오브젝트의 상호작용이 누르는 동안 반복돠야한다면
            {
                interactionCoroutine = StartCoroutine(InteractionCoroutine());
            }
            else //오브젝트의 상호작용이 한번만 발동한다면
            {
                interactionObject.OnClickInteract();
            }
        }
        else if(!value.isPressed && interactionObject != null) //클릭에서 땟을때 발생
        {
            if (interactionObject.Continuous()) //반복 상호작용 끝내기
            {
                StopCoroutine(interactionCoroutine);
            }
            interactionObject = null;
        }
    }
    
    //반복 상호작용
    IEnumerator InteractionCoroutine()
    {
        while (true)
        {
            interactionObject.OnClickInteract();

            yield return null;
        }
    }
}
