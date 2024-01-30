using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class NPCMovement : MonoBehaviour
{

    [Header("Script")]
    private NPCSetting npcSetting;
    private MovementController movementController;
    

    [Header("installation")]
    [SerializeField] List<GameObject> installationList;
    GameObject counter;

    [SerializeField] private bool buying; // 구매여부
    [SerializeField] bool goCounter; // 카운터를 목적지로 정하는 여부
    [SerializeField] bool goDoor;//문으로 갈지 여부

    [SerializeField] GameObject bestMachine; // 가장 가까운 진열대
    [SerializeField] float installationListPosition; // NPC와 게임 오브젝트 간의 거리 절대값
    [SerializeField] int bestPosition_num; // 찾아가야할 게임 오브젝트 List 인덱스

    private GameObject hitObject; // 콜라이더로 부딪힌 진열대
    private ItemSO itemToBuy;


    void Start()
    {
        SettingInstallations(); // 카운터와 NPC 소환 기준으로 현재 진열대 게임 오브젝트의 리스트
        InintSetting(); // 변수 초기화
        MachinePositionInform(); //가장 가까운 진열대 찾기

    }

    private void Update()
    {
        if (movementController.isMove == false)
        {
            arriveNomuchine();
        }
    }

    private void InintSetting()
    {
        // 변수 초기화 및 겟 컴퍼넌트

        goCounter = false;
        buying = false;
        goDoor = false;

        npcSetting = GetComponent<NPCSetting>();
        movementController = GetComponent<MovementController>();
        movementController.speed = npcSetting.npcSo.speed;

    }

    private void SettingInstallations()
    {
        // 카운터와 현재 진열대 리스트 가져오기

        counter = GameManager.instance.dataManager.counter;
        //installationList = GameManager.instance.dataManager.curSellInstallations;

        foreach(var item in GameManager.instance.dataManager.curInstallations)
        {
            Debug.Log("넣는당1");
            if (item.GetComponent<InstallationController>()._installationData.id == 5)
            {
                Debug.Log("넣는당");
                installationList.Add(item);
                Debug.Log("넣었당");
            }
        }

    }

    void arriveNomuchine()
    {
        MachinePositionInform();
        if (movementController.isMove == false && installationList != null)
        {
            // 도착했을 때, 부딪히는 진열대가 없을 경우 (플레이어가 중간에 삭제했을때)
            installationList.Remove(bestMachine);
            MachinePositionInform();

        }

        if (goDoor == true)
        {
            bestMachine = GameManager.instance.spawnManager.door;
            movementController.destinationObj = bestMachine;
        }
        

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (goCounter == true)
        {
            GameManager.instance.poolManager.DeSpawnFromPool(this.gameObject);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {

        hitObject = collision.gameObject; // 부딪힌 게임 오브젝트

        if (goCounter == false && buying == false && hitObject == bestMachine)
        {
            // 진열대에 부딪혔을 때
            Debug.Log(hitObject.name);

            foreach (var items in hitObject.GetComponentInChildren<AbstractInventory>().Items)
            {
                // npc가 구매하고 싶은 빵이 진열대에 있을 때, 1개 빼내기
                ItemSO itemso = items.Key;
                if (npcSetting.selectedFavoriteFoodID == itemso.id)
                {
                    Debug.Log(hitObject.GetComponentInChildren<AbstractInventory>().inventoryID);
                    buying = true;
                    itemToBuy = itemso;
                }
            }
            GameManager.instance.inventoryManager.RemoveItemFromInventory(hitObject.GetComponentInChildren<AbstractInventory>().inventoryID, itemToBuy, 1);

            if (installationList.Count == 1)
            {
                // 마지막 진열대를 들렀을 때
                installationList.Clear();
                goCounter = true;

            }
            else
            {
                installationList.RemoveAt(bestPosition_num);
            }



        }

        else if (hitObject.name == "counter")
        {
            // 카운터에 부딪혔을 때
            movementController.speed = 0;
            if (buying == true)
            {
                Debug.Log("삼");
                GameManager.instance.statManager.EarnGold(npcSetting.selectedFavoriteFood.price);
            }
            else
            {
                Debug.Log("안삼");
            }
            movementController.speed = npcSetting.npcSo.speed;
            goDoor = true;

        }

        hitObject = null;
        arriveNomuchine(); //도착했을 때, 부딪힌 게임 오브젝트가 있는지 다시 목적지 설정 (하지만 오브젝트가 사라지면 부딪힌걸 판단 못하지 않나?)

    }


    void MachinePositionInform()
    {

        installationListPosition = 0f;
        bestPosition_num = 0;

        if (goCounter == false)
        {
            // 카운터로 간다는 값이 없을 때

            if (installationList.Count > 1)
            {
                // 현재 들리지 않은 진열대가 2개 이상일때 작동

                for (int i = 0; i < installationList.Count; i++)
                {
                    //반복문을 통해 전체 리스트에서 가장 가까운 값을 검색

                    Vector2 pos = this.transform.position - installationList[i].transform.position; // 해당 게임 오브젝트 - 기계간의 거리 계산 값
                    float positionNum = Mathf.Abs(pos.y) + Mathf.Abs(pos.x);
                    if (installationListPosition == 0)
                    {
                        // 만약 i == 0일때와 같은 뜻의 if문, installationListPosition은 처음에 0으로 초기화됨
                        installationListPosition = positionNum;
                        bestPosition_num = i;
                        // 그때 그 첫번째가 가장 가까운 거라고 체크 (인덱스에서 가져옴)

                    }
                    else if (installationListPosition > positionNum)
                    {
                        installationListPosition = positionNum;
                        bestPosition_num = i;
                        // 만약 그 앞전것 보다 현재 인덱스가 가깝다면 그것으로 갱신
                    }

                }

            }
            else
            {
                // 들리지 않은 진열대 리스트가 1개 이하일 때
                bestPosition_num = 0;
            }

            bestMachine = installationList[bestPosition_num];
            // 들려야하는 진열대는 위에서 정한 인덱스 넘버


        }
        else
        {
            bestMachine = counter;

        }
        movementController.destinationObj = bestMachine;
    }
}
