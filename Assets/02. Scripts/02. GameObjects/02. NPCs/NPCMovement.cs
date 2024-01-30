using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class NPCMovement : MonoBehaviour
{

    
    [SerializeField] InventoryManager inventoryManager;
    // TODO 이걸 다른 곳에서 리스트 받아와야함
    [SerializeField] List<GameObject> installationList;
    GameObject counter;

    [SerializeField] float installationListPosition;
    [SerializeField] int bestPosition_num; // 찾아가야할 게임 오브젝트 List 인덱스

    [SerializeField] private bool buying;
    [SerializeField] bool goCounter;

    public GameObject machineList;
    [SerializeField] GameObject bestMachine;

    private int layerMask;
    private GameObject hitObject;
    private NPCSetting npcSetting;
    private MovementController movementController;

    // Start is called before the first frame update
    void Start()
    {

        //SettingInstallations(); // 카운터와 NPC 소환 기준으로 현재 진열대 게임 오브젝트의 리스트
        // 여기서 진열대만 찾게 설정 (ID로)
        goCounter = false;
        buying = false;
        layerMask = LayerMask.GetMask("Interior");
        
        npcSetting = GetComponent<NPCSetting>();
        movementController = GetComponent<MovementController>();
        movementController.speed = npcSetting.npcSo.speed;
        MachinePositionInform();
        

    }

    //private void SettingInstallations()
    //{
    //    counter = GameManager.instance.dataManager.counter;
    //    foreach (var num in GameManager.instance.dataManager.curInstallations)
    //    {
    //        if (num._installationData.id)
    //        {
    //            installationList.Add(num);
    //        }
    //    }
    //} 인스톨레이션 데이터에서 진열장만 가져오는 코드였으나 데이터 메니저에서 진열장만 따로 빼내게

    void arriveNomuchine()
    {
        // TODO NPC가 리스폰 되었을 때 기준 매대 게임오브젝트 리스트를 불러오는 값


        if (movementController.speed == 0 && installationList != null)
        {

            installationList.Remove(bestMachine);
            MachinePositionInform();

        }
        // 도착했을 때, 부딪히는 진열대가 없을 경우 (플레이어가 중간에 삭제했을때)


        if (goCounter == true)
        {
            movementController.destinationObj = counter;
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

            if (installationList.Count == 1)
            {
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
            movementController.speed = 0;
        }
        hitObject = null;
        MachinePositionInform();
        arriveNomuchine();


        //    //TODO 디스트로이 될때, 값 리셋 시켜주기
    }


    void MachinePositionInform()
    {
       
        installationListPosition = 0f;
        bestPosition_num = 0;

        if (installationList.Count > 0 && goCounter == false)
        {
            if (installationList.Count>1)
            { 
                for (int i = 0; i < installationList.Count; i++)
                {
                    Vector2 pos = this.transform.position - installationList[i].transform.position; // 해당 게임 오브젝트 - 기계간의 거리 계산 값
                    float positionNum = Mathf.Abs(pos.y) + Mathf.Abs(pos.x);
                    if (installationListPosition == 0)
                    {
                        installationListPosition = positionNum;
                        bestPosition_num = i;

                    }
                    else if (installationListPosition > positionNum)
                    {
                        installationListPosition = positionNum;
                        bestPosition_num = i;
                    }

                }
            }
            else
            {
                bestPosition_num = 0;
            }
            bestMachine = installationList[bestPosition_num];
            

        }
        else
        {
            movementController.speed = 0f;

        }
        movementController.destinationObj = bestMachine;
    }
}
