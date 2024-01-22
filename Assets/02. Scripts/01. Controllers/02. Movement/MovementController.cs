using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementController : MonoBehaviour
{
    [FormerlySerializedAs("_itemData")] public InteractionObject interactionData;
    
    private void Awake()
    {
        interactionData = gameObject.GetComponent<InteractionObject>();
    }

    public void Move(GameObject selectObject)
    {
        interactionData._interactionData._interactionStat.destinationGameObject = selectObject;
        
        if (interactionData._interactionData._interactionStat.destinationGameObject)
        {
            StartCoroutine("Movement");
        }
        else
        {
            Debug.Log("Please Select Destination Object");
        }
    }

    public virtual IEnumerator Movement()
    {
        float distance = Vector2.Distance(interactionData._interactionData._interactionStat.destinationGameObject.transform.position, gameObject.transform.position);//도착지에서 시작지점까지 거리
        float duration = distance / interactionData._interactionData._interactionStat.speed; // 이동에 걸리는 전체 시간
        float lerpTime = 0f; //0 ~ 1 증가 값

        while (lerpTime < 1f)
        {
            lerpTime += Time.deltaTime / duration;
            gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, interactionData._interactionData._interactionStat.destinationGameObject.transform.position, lerpTime); //

            yield return null; // 한 프레임 대기
            
            if (Vector2.Distance(gameObject.transform.position, interactionData._interactionData._interactionStat.destinationGameObject.transform.position) < 0.1f)
            {
                break;
            }
        }
    }
}
