using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private int favoriteFood;
    [SerializeField] private bool buying;

    private RaycastHit2D hit;

    private int layerMask;
    private GameObject hitObject;

    // Start is called before the first frame update
    void Awake()
    {
        favoriteFood = 1;
        buying = false;
        layerMask = LayerMask.GetMask("Interior");
    }


    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, new Vector3(-1, 0, 0) * 0.9f, new Color(0, 1, 0), layerMask); //TODO 레이캐스트 크기와 위치 조정 필요

        hit = Physics2D.Raycast(transform.position, new Vector3(-1, 0, 0) * 0.9f, layerMask); //TODO 레이캐스트 크기와 위치 조정 필요 (레이캐스트 포지션이나 크기 변수 지정)

        if (hit.collider != null&&buying==false)
        {
            hitObject = hit.collider.gameObject; // 레이에 닿은 오브젝트 가져오기
            Debug.Log(hitObject.name);
            if (hitObject.GetComponent<MarketStanding>().food == favoriteFood) // 매대의 음식과 기호 음식이 같을때
            {
                buying = true; // 구매 했다는 표기
                hitObject.GetComponent<MarketStanding>().foodnum -= 1; // 매대의 음식 갯수 줄임
            }
        }


        //TODO 디스트로이 될때, 값 리셋 시켜주기
    }

}
