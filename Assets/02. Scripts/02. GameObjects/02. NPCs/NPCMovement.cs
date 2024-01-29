using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class NPCMovement : MonoBehaviour
{

    [SerializeField] private bool buying;
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] List<GameObject> machineObject; // TODO 이걸 다른 곳에서 리스트 받아와야함
    [SerializeField] float machineObjectPosition;
    [SerializeField] int bestPosition_num; // 찾아가야할 게임 오브젝트 List 인덱스
    [SerializeField] bool goCounter;
    [SerializeField] bool arriveCounter;

    public GameObject machineList;
    [SerializeField] GameObject bestMachine;
    [SerializeField] GameObject counter;

    private int layerMask;
    private GameObject hitObject;
    private NPCSetting npcSetting;
    private MovementController movementController;

    // Start is called before the first frame update
    void Start()
    {
        machineObject = machineList.GetComponent<MaketStandingList>().maketStandingList;
        counter = machineList.GetComponent<MaketStandingList>().counter;
        goCounter = false;
        buying = false;
        arriveCounter = false;
        layerMask = LayerMask.GetMask("Interior");
        npcSetting = GetComponent<NPCSetting>();
        movementController = GetComponent<MovementController>();
        movementController.speed = npcSetting.npcSo.speed;
        MachinePositionInform();
        

    }

    void arriveNomuchine()
    {
        // TODO NPC가 리스폰 되었을 때 기준 매대 게임오브젝트 리스트를 불러오는 값


        if (movementController.speed == 0 && machineObject != null)
        {

            machineObject.Remove(bestMachine);
            MachinePositionInform();

        }
        // 도착했을 때, 부딪히는 진열대가 없을 경우 (플레이어가 중간에 삭제했을때)


        if (goCounter == true)
        {
            movementController.destinationObj = counter;
        }

        if (arriveCounter == true)
        {
            movementController.speed = 0;
        }


        // 만약 도중에 유저가 오브젝트를 삭제했다면? 도착했을때 레이에 부딪히는게 없다면 해당 게임오브젝트를 삭제
        // 위치에 도착하면 멈추기 때문에, is Move 펄스로 판단
        //단 머신 오브젝트로 판정함으로, 머신 오브젝트가 0일경우의 수도 상정해야함 (카운터로 간다 등)

    }




    void OnCollisionEnter2D(Collision2D collision)
    {
        
        hitObject = collision.gameObject;

        if (buying == false && hitObject == bestMachine)
        {
            Debug.Log(hitObject.name);

            foreach (var items in hitObject.GetComponent<AbstractInventory>().Items)
            {
                ItemSO itemso = items.Key;
                if (npcSetting.selectedFavoriteFoodID == itemso.id)
                {
                    inventoryManager.RemoveItemFromInventory(hitObject.GetComponent<AbstractInventory>().inventoryID, itemso, 1);
                    buying = true;
                }

            }
            Debug.Log("부딪혔당");

            if (machineObject.Count == 1)
            {
                machineObject.Clear();
                goCounter = true;
                
            }
            else
            {
                machineObject.RemoveAt(bestPosition_num);
            }
            


        }

        else if (hitObject.name == "counter")
        {
            arriveCounter = true;
        }
        hitObject = null;
        MachinePositionInform();
        arriveNomuchine();


        //    //TODO 디스트로이 될때, 값 리셋 시켜주기
    }


    void MachinePositionInform()
    {
       
        machineObjectPosition = 0f;
        bestPosition_num = 0;

        if (machineObject.Count > 0 && goCounter == false)
        {
            if (machineObject.Count>1)
            { 
                for (int i = 0; i < machineObject.Count; i++)
                {
                    Vector2 pos = this.transform.position - machineObject[i].transform.position; // 해당 게임 오브젝트 - 기계간의 거리 계산 값
                    float positionNum = Mathf.Abs(pos.y) + Mathf.Abs(pos.x);
                    if (machineObjectPosition == 0)
                    {
                        machineObjectPosition = positionNum;
                        bestPosition_num = i;

                    }
                    else if (machineObjectPosition > positionNum)
                    {
                        machineObjectPosition = positionNum;
                        bestPosition_num = i;
                    }

                }
            }
            else
            {
                bestPosition_num = 0;
            }
            bestMachine = machineObject[bestPosition_num];
            

        }
        else
        {
            movementController.speed = 0f;

        }
        movementController.destinationObj = bestMachine;
    }
}
