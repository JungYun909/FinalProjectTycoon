using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private int favoriteFood;
    [SerializeField] private bool buying;
    [SerializeField]  List<GameObject> machineObject; // TODO 이걸 다른 곳에서 리스트 받아와야함
    [SerializeField] float machineObjectPosition;
    [SerializeField] int bestPosition_num; // 찾아가야할 게임 오브젝트 List 인덱스

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

    void OnEnable()
    {
        // TODO NPC가 리스폰 되었을 때 기준 매대 게임오브젝트 리스트를 불러오는 값
        // 만약 도중에 유저가 오브젝트를 삭제했다면? 도착했을때 레이에 부딪히는게 없다면 해당 게임오브젝트를 삭제
        MachinePositionInform();
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
    
            machineObject.Remove(hitObject);
            MachinePositionInform();
        }


        //TODO 디스트로이 될때, 값 리셋 시켜주기
    }
    // 테스트용

    void ArriveMachine()
    {
        Debug.DrawRay(transform.position, new Vector3(-1, 0, 0) * 0.9f, new Color(0, 1, 0), layerMask); //TODO 레이캐스트 크기와 위치 조정 필요

        hit = Physics2D.Raycast(transform.position, new Vector3(-1, 0, 0) * 0.9f, layerMask); //TODO 레이캐스트 크기와 위치 조정 필요 (레이캐스트 포지션이나 크기 변수 지정)

        if (hit.collider == null)
        {
            //만약 도착 후, 레이에 부딪힌게 없다면 오브젝트 삭제 (가장 가까운 순으로 도착이기에 도착한 지점이 해당 오브젝트가 있던 지점)
            machineObject.RemoveAt(bestPosition_num);
        }
        else if (hit.collider != null && buying == false)
        {
            hitObject = hit.collider.gameObject; // 레이에 닿은 오브젝트 가져오기
            Debug.Log(hitObject.name);
            if (hitObject.GetComponent<MarketStanding>().food == favoriteFood) // 매대의 음식과 기호 음식이 같을때
            {
                buying = true; // 구매 했다는 표기
                hitObject.GetComponent<MarketStanding>().foodnum -= 1; // 매대의 음식 갯수 줄임
            }

            machineObject.Remove(hitObject);
        }

        MachinePositionInform();

    }

    void MachinePositionInform()
    {
        machineObjectPosition = 0f;
        bestPosition_num = 0;

        if (machineObject!=null)
        { 
            for (int i = 0; i < machineObject.Count; i++)
            {
                Vector2 pos = this.transform.position - machineObject[i].transform.position; // 해당 게임 오브젝트 - 기계간의 거리 계산 값
                float positionNum = Mathf.Abs(pos.y) + Mathf.Abs(pos.x);
                if (machineObjectPosition == 0)
                {
                    machineObjectPosition = positionNum;
                
                }
                else if (machineObjectPosition> positionNum)
                {
                    machineObjectPosition = positionNum;
                    bestPosition_num = i;                
                }
            
            }

        }

    }

}
