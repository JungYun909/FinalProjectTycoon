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
    
    [Header("InteractionOBJ")]
    private GameObject curInteractGameobject;
    private IInteractable curInteractable;
    
    //상호작용 상속받아 사용 가능한 오브젝트의 기능을 실행하고 초기화
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
}