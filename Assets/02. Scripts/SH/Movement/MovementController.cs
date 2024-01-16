using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementController : MonoBehaviour
{
    public GameObject tempStartGameObject;//TODO 나중에 수정
    private GameObject curGameObject;
    public GameObject tempEndGameObject;//TODO 나중에 수정
    
    public MovementObject movementObject;
    private void Awake()
    {
        movementObject.InitSetting();
        curGameObject = gameObject;
        // movementObject.Spawn(movementObject.moveData.moveObj, tempStartGameObject);

    }

    private void Start()
    {
        StartCoroutine(Movement(gameObject, tempStartGameObject, tempEndGameObject, movementObject.moveData.speed));
    }

    private void Update()
    {
        movementObject.DeSpawn(gameObject, tempEndGameObject);
    }

    private IEnumerator Movement(GameObject moveObj ,GameObject startObj, GameObject endObj, float speed)
    {
        float distance = Vector2.Distance(endObj.transform.position, startObj.transform.position);//도착지에서 시작지점까지 거리
        float duration = distance / speed; // 이동에 걸리는 전체 시간
        float lerpTime = 0f; //0 ~ 1 증가 값

        while (lerpTime < 1f)
        {
            lerpTime += Time.deltaTime / duration;
            moveObj.transform.position = Vector2.Lerp(moveObj.transform.position, endObj.transform.position, lerpTime); //

            yield return null; // 한 프레임 대기
            
            if (Vector2.Distance(moveObj.transform.position, endObj.transform.position) < 0.1f)
            {
                break;
            }
        }
    }
}
