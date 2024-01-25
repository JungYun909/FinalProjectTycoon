using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class NPCMovement : MonoBehaviour
{
    private System.Random random;
    [SerializeField] NPCSetting npcSetting;
    [SerializeField] NpcSO npcSO;

    [SerializeField] private List<int> favoriteFood;
    [SerializeField] private int selectedFavoriteFoodID;
    [SerializeField] private bool buying;

    [SerializeField] InventoryManager inventoryManager;

    [SerializeField]  List<GameObject> machineObject; // TODO 이걸 다른 곳에서 리스트 받아와야함
    [SerializeField] Vector2 machineWaypoint;
    [SerializeField] float machineObjectPosition;
    [SerializeField] int bestPosition_num; // 찾아가야할 게임 오브젝트 List 인덱스
    MovementController movementController;
    
    [SerializeField] GameObject bestMachine;
    [SerializeField] GameObject counterMachine;
    private RaycastHit2D hit;

    private int layerMask;
    private GameObject hitObject;

    // Start is called before the first frame update
    void Awake()
    {
        random = new System.Random();
        npcSetting = GetComponent<NPCSetting>();
        favoriteFood = npcSetting.npcSo.favoriteFood;
        buying = false;
        layerMask = LayerMask.GetMask("Interior");
        movementController = GetComponent<MovementController>();
        SelectFaveoriteFood();
        MachinePositionInform();

    }

    void OnEnable()
    {
        // TODO NPC가 리스폰 되었을 때 기준 매대 게임오브젝트 리스트를 불러오는 값
        // 만약 도중에 유저가 오브젝트를 삭제했다면? 도착했을때 레이에 부딪히는게 없다면 해당 게임오브젝트를 삭제


    }



    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, new Vector3(-1, 0, 0) , new Color(0, 1, 0), layerMask); //TODO 레이캐스트 크기와 위치 조정 필요

        hit = Physics2D.Raycast(transform.position, new Vector3(-1, 0, 0), layerMask); //TODO 레이캐스트 크기와 위치 조정 필요 (레이캐스트 포지션이나 크기 변수 지정)


        if (hit.collider != null && buying == false)
        {
            hitObject = hit.collider.gameObject; // 레이에 닿은 오브젝트 가져오기
            Debug.Log(hitObject.name);
            if (hitObject = bestMachine)
            {
                Debug.Log(hitObject.name);

                foreach (var items in hitObject.GetComponent<AbstractInventory>().Items)
                {
                    ItemSO itemso = items.Key;
                    if (selectedFavoriteFoodID == itemso.id)
                    {
                        inventoryManager.RemoveItemFromInventory(hitObject.GetComponent<AbstractInventory>().inventoryID, itemso, 1);
                        buying = true;
                    }

                }
                machineObject.Remove(hitObject);

            }
            
            MachinePositionInform();
          
        }


        //TODO 디스트로이 될때, 값 리셋 시켜주기
    }

    void SelectFaveoriteFood()
    {
        int num = random.Next(0, favoriteFood.Count);
        selectedFavoriteFoodID = favoriteFood[num];

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

            bestMachine = machineObject[bestPosition_num];
            NPCMovementRutine();

        }
        else
        {
            movementController.Move(counterMachine);
        }


    }

    void NPCMovementRutine()
    {
        if (buying == false)
        {
            movementController.Move(bestMachine);
        }

        else
        {
            movementController.Move(counterMachine);
        }
        
    }

}
