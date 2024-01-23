using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public interface IInteractable
{
    bool Continuous();
    void OnClickInteract();
    void OnColliderInteract();
}
public class InteractionManager : MonoBehaviour
{
    //상호작용 오브젝트 정보저장
    public GameObject curGameObject;
    public GameObject targetGameObject; 
    private IInteractable interactionObject;
    
    //상호작용에 필요한 변수저장
    private Vector2 curMouseDirection;
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
        //정보 초기화
        bool isCurClick = false;
        
        //레이로 상호작용 객체 감지시도
        RaycastHit2D ray = Physics2D.Raycast(curMouseDirection, Vector2.zero, 0f);
        
        //레이가 상호작용 하지 않는 객체 감지시 리턴
        if( !ray.collider || ray.collider.gameObject.GetComponent<IInteractable>() == null)
            return;
        //레이가 감지한 객체의 상호작용 정보 저장
        interactionObject = ray.collider.gameObject.GetComponent<IInteractable>();
        
        //클릭 중일떄 발생
        if (value.isPressed)
        {
            if (interactionObject.Continuous()) //오브젝트의 상호작용이 누르는 동안 반복돠야한다면
            {
                interactionCoroutine = StartCoroutine(InteractionCoroutine());
            }
            else if(isCurClick == false) //오브젝트의 상호작용이 한번만 발동한다면
            {
                interactionObject.OnClickInteract();
                isCurClick = true;
            }
        }
        else if(!value.isPressed) //클릭에서 땟을때 발생
        {
            if (interactionObject.Continuous()) //반복 상호작용 끝내기
            {
                StopCoroutine(interactionCoroutine);
            }
            else if(isCurClick) //다시 한번 발동할 준비
            {
                isCurClick = false;
            }
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
