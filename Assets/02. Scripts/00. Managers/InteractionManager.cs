using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = System.Random;

public interface IInteractable
{
    bool Continuous();
    void OnClickInteract();
    void OffClickInteract();
    void OnColliderInteract();
}
public class InteractionManager : MonoBehaviour
{
    //상호작용에 필요한 변수저장
    public IInteractable interactionObject;
    public Vector2 curMouseDirection;
    private Coroutine interactionCoroutine;

    public int installationFunctionIndex = 0;

    public string targetID;

    public event Action onTuto;
    //마우스 포지션 바뀔때마다 정보갱신
    public void OnLook(InputValue value)
    {
        curMouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    //마우스 눌렀을때 상호작용 관리
    //public void OnClick(InputValue value)
    //{
    //    PointerEventData data = new PointerEventData(EventSystem.current);
    //    data.position = Input.mousePosition;
    //    List<RaycastResult> results = new List<RaycastResult>();
    //    EventSystem.current.RaycastAll(data, results);

    //    bool uiIntercepted = false;

    //    if (results.Count > 0)
    //    {
    //        foreach (RaycastResult r in results)
    //        {
    //            if (r.gameObject.GetComponent<TutorialMissionID>() == null)
    //                continue;
    //            if (r.gameObject.GetComponent<TutorialMissionID>().missionID == targetID)
    //            {
    //                r.gameObject.GetComponent<Button>().onClick.Invoke();
    //                onTuto?.Invoke();
    //            }
    //        }
    //    }


    //    //클릭 중일떄 발생
    //    if (value.isPressed && interactionObject == null)
    //    {
    //        RaycastHit2D ray = Physics2D.Raycast(curMouseDirection, Vector2.zero, 0f, LayerMask.GetMask("Installation"));
    //        if( !ray.collider || ray.collider.gameObject.GetComponent<IInteractable>() == null)
    //            return;

    //        interactionObject = ray.collider.gameObject.GetComponent<IInteractable>();
    //        if (interactionObject.Continuous()) //오브젝트의 상호작용이 누르는 동안 반복돠야한다면
    //        {
    //            interactionCoroutine = StartCoroutine(InteractionCoroutine());
    //        }
    //        else //오브젝트의 상호작용이 한번만 발동한다면
    //        {
    //            interactionObject.OnClickInteract();
    //        }
    //    }
    //    else if(!value.isPressed && interactionObject != null) //클릭에서 땟을때 발생
    //    {
    //        if (interactionObject.Continuous()) //반복 상호작용 끝내기
    //        {
    //            StopCoroutine(interactionCoroutine);
    //        }
    //        interactionObject.OffClickInteract();
    //        interactionObject = null;
    //    }
    //}

    public void OnClick(InputValue value)
    {
        PointerEventData data = new PointerEventData(EventSystem.current);
        data.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);

        bool uiIntercepted = false;

        // 튜토리얼이 종료되었을 경우 UI 오브젝트에 의한 상호작용 방지
        bool tutorialCompleted = GameManager.instance.dataManager.playerData.tutoClear;

        if (value.isPressed && results.Count > 0 && tutorialCompleted)
        {
            foreach (RaycastResult r in results)
            {
                // UI 오브젝트를 감지하면 상호작용 중단
                if (r.gameObject.layer == LayerMask.NameToLayer("UI"))
                {
                    uiIntercepted = true;
                    break; // UI 오브젝트가 클릭되었으므로 더 이상의 처리를 중단
                }
            }
        }

        if (value.isPressed && !tutorialCompleted)
        {
            // 튜토리얼 중일 때의 로직, 모든 상호작용 허용
            foreach (RaycastResult r in results)
            {
                TutorialMissionID missionIDComponent = r.gameObject.GetComponent<TutorialMissionID>();
                if (missionIDComponent != null && missionIDComponent.missionID == targetID)
                {
                    r.gameObject.GetComponent<Button>().onClick.Invoke();
                    Debug.Log("1");
                    onTuto?.Invoke();
                    return; // 튜토리얼 미션 처리 후 바로 반환
                }
            }
        }

        // UI 오브젝트가 클릭된 경우 뒤의 로직을 수행하지 않음
        if (uiIntercepted)
        {
            return;
        }

        // 클릭 중일 때 발생하는 상호작용
        if (value.isPressed && interactionObject == null)
        {
            RaycastHit2D ray = Physics2D.Raycast(curMouseDirection, Vector2.zero, 0f, LayerMask.GetMask("Installation"));
            if (!ray.collider || ray.collider.gameObject.GetComponent<IInteractable>() == null)
                return;

            interactionObject = ray.collider.gameObject.GetComponent<IInteractable>();
            if (interactionObject.Continuous()) // 오브젝트의 상호작용이 누르는 동안 반복되어야 한다면
            {
                interactionCoroutine = StartCoroutine(InteractionCoroutine());
            }
            else // 오브젝트의 상호작용이 한 번만 발동한다면
            {
                interactionObject.OnClickInteract();
            }
        }
        else if (!value.isPressed && interactionObject != null) // 클릭에서 뗐을 때 발생
        {
            if (interactionObject.Continuous()) // 반복 상호작용 끝내기
            {
                StopCoroutine(interactionCoroutine);
            }
            interactionObject.OffClickInteract();
            interactionObject = null;
        }
    }

    //반복 상호작용
    IEnumerator InteractionCoroutine()
    {
        while (interactionObject != null)
        {
            interactionObject.OnClickInteract();

            yield return null;
        }
    }
}
