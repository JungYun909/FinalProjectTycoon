using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour                      //TODO Update까지? > 마찬가지로 delegate> 전달받아서 바꾼? UI창 요소들을 어떻게 받아올 것인가 고민 필요
{
    [Header("UI Classification")]
    [SerializeField] private UIBase dailyResultWindow;    // 일일정산창 - 모든 UI중 유일하게 플레이어의 명령이나 상호작용이 아닌 특별한 로직(시간 기반)으로 제어해야 하므로 별도로 할당[
    private UIBase currentDailyResultWindow;
    [SerializeField] private List<UIBase> uiAlwaysOn = new List<UIBase>();   //상하단에 항상 위치하는 UI요소의 리스트 - UIStack으로 관리하지 않을 대상
    [SerializeField] private List<UIBase> uiList = new List<UIBase>();  // 일반적인 UI창 - uiStack으로 관리할 대상들
    private Stack<UIBase> uiStack = new Stack<UIBase>();   // UI창 스택

    [SerializeField] private float dailyResultOpenInterval = 60f; // 정산창을 띄우기 위한 주기. 서버사용 없을 땐 약 5분 / 서버일 땐 하루단위 (86400초)로 설6

    [Header("For Inventory UI Update")]
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GameObject itemLinePrefab;

    //UI오픈에 반응하는 델리게이트 / 이벤트 선언하기 위한 필드
    public delegate void DailyResultWindowOn();
    public event DailyResultWindowOn OnDailyWindowOpen;
    
    public void Initialize()
    {
        //InitUIList();   // 매니저 활성화시 전체 UI창 초기화 진행
        //StartCoroutine(DailyResultWindowRoutine());
        //OpenPermanentWindows(uiAlwaysOn);
        GameManager.instance.statManager.onDateChanged += CheckSceneAndOpenDailyResultWindow;
        GameManager.instance.sceneManager.sceneInfo += HandleScene;
        Debug.Log("Subscribed");
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        GameManager.instance.sceneManager.sceneInfo -= HandleScene;
        GameManager.instance.statManager.onDateChanged -= CheckSceneAndOpenDailyResultWindow;
    }

    private void HandleScene(SceneType scene)
    {
        Debug.Log("SceneHandled");
        if (scene != SceneType.TitleScene)
        {
            OpenPermanentWindows(uiAlwaysOn);
        }
    }
    public void OpenPermanentWindows(List<UIBase> permaUI)
    {
        
        foreach (UIBase uiWindow in permaUI)
        {
            Instantiate(uiWindow, transform);
        }
    }

    public void OpenWindow(UIBase uiPrefab, bool keepPreviousWindow = false, AbstractInventory inventory = null)
    {
        UIBase uiInstance = Instantiate(uiPrefab, transform).GetComponent<UIBase>();
        if (uiInstance is InventoryShow inventoryShow && inventory != null)
        {
            inventoryShow.OpenInventory(inventory);
        }
        if (uiStack.Count > 0 && !keepPreviousWindow)    //처음 열리는 창이 아닐 때에는 
        {
            uiStack.Peek().gameObject.SetActive(false);   // 기존에 열려있던 창을 비활성화. Peek()이란 스택 맨 위를 확인하는 메서.
        }
        uiStack.Push(uiInstance);  //ui프리팹을 열어줌
        uiInstance.UpdateUI();
    }

    public void CloseAll() // 스택으로 관리되는 창 전체 닫기 위한 로지
    {
        while (uiStack.Count > 0)
        {
            UIBase currentUI = uiStack.Pop();
            Destroy(currentUI.gameObject);
        }
    }

    public void GoBack()     //뒤로가기 버튼용
    {
        if (uiStack.Count > 1)
        {
            UIBase curUIWindow = uiStack.Pop();    //uiStack에서 
            Destroy(curUIWindow.gameObject);       //현재 열려있는 창 파괴
            uiStack.Peek().gameObject.SetActive(true); //이전에 Stack에 저장된 ui창을 true로 돌림
        }
    }

    public void DestroyUIObject(GameObject objectToDestroy)
    {
        Destroy(objectToDestroy);
    }

    private void InitUIList()    // 매니저로 관리할 모든 UI요소들의 초기화 일제 실행. 추후 UI매닉저가 싱글톤이 아니어도 InitUIList를 통해 전체 UI에게 초기화 명령을 내릴 수 있.
    {
        dailyResultWindow.Initialize();
        foreach (UIBase uiWindow in uiAlwaysOn)
        {
            uiWindow.Initialize();
        }

        foreach (UIBase uiWindow in uiList)
        {
            uiWindow.Initialize();
        }
    }

    private IEnumerator DailyResultWindowRoutine()   //코루틴으로 일일정산창UI 열기 관리
    {
        while (true)
        {
            yield return new WaitForSeconds(dailyResultOpenInterval);
            OpenDailyResultWindow();
        }
    }

    public void OpenDailyResultWindow()
    {
        if (currentDailyResultWindow != null)
        {
            Destroy(currentDailyResultWindow.gameObject);    //혹시나 이미 열려있는 상태일때 중첩되지 않도록 이전에 열린 창은 파기
        }
        currentDailyResultWindow = Instantiate(dailyResultWindow, transform);    //currentDailyResultWindow 생성
        currentDailyResultWindow.Initialize();  // 
        OnDailyWindowOpen?.Invoke();    // 일일정산창 열리면 이벤트를 발생시킴. 여기서는 
    }

    private void CheckSceneAndOpenDailyResultWindow()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "TitleScene")
        {
            OpenDailyResultWindow();
        }
    }
}

//    public void UpdateInventoryUI(Dictionary<ItemSO, int> items, GameObject gameObject)
//    {
//        //UIToChange uiToChange = gameObject.GetComponentInChildren<UIToChange>();
//       // Transform contentPanel = uiToChange.transform; 
//        foreach (Transform child in contentPanel)
//        {
//            Destroy(child.gameObject);
//        }

//        GameObject currentLine = null;
//        int slotIndex = 0;

//        foreach (var entry in items)
//        {
//            // 새로운 라인이 필요한 경우 생성
//            if (slotIndex % 5 == 0)
//            {
//                currentLine = Instantiate(itemLinePrefab, contentPanel);
//            }

//            // 아이템 슬롯 생성 및 설정
//            GameObject slotObject = Instantiate(itemSlotPrefab, currentLine.transform);
//            ItemSlotInfo slotInfo = slotObject.GetComponent<ItemSlotInfo>();
//            slotInfo.Setup(entry.Key, entry.Value);

//            slotIndex++;
//        }
//    }
//}


// TODO : UI 목록을 리스트가 아닌 딕셔너리로 바꾸는 것 고려
// 또한 동일한 UI는 한번에 한 개의 오브젝트만 생성되도록 바꿀 필요가 있음. 현재 OpenWindow는 이전에 열렸던 모든 UI를 
// 동일한 UI창이 열렸던 만큼 SetActive = false인 상태로 씬에 존재하므로 메모리 사용 측면에서 손해가 생길 수밖에 없는 구조로 사료됨
// 덧붙여 현재는 UI창이 통채로 프리팹으로 만들어지므로 (화면마다 공통되는 요소가 반복됨) 반복되는 요소를 줄이기 위해 가장 처음에 발생하는 
// 1번 스택에 위치한 UI창은 SetActive = false로 되지 않도록 바꾸고 이후 열리는 하위 UI창이 1번 스택인 최상위 창 안에서 생성되도록 바꿀 예정
