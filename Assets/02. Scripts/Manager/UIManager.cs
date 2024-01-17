using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Classification")]
    [SerializeField] private UIBase dailyResultWindow;    // 일일정산창 - 모든 UI중 유일하게 플레이어의 명령이나 상호작용이 아닌 특별한 로직(시간 기반)으로 제어해야 하므로 별도로 할당[
    private UIBase currentDailyResultWindow;
    [SerializeField] private List<UIBase> uiAlwaysOn = new List<UIBase>();   //상하단에 항상 위치하는 UI요소의 리스트 - UIStack으로 관리하지 않을 대상
    [SerializeField] private List<UIBase> uiList = new List<UIBase>();  // 일반적인 UI창 - uiStack으로 관리할 대상들
    private Stack<UIBase> uiStack = new Stack<UIBase>();   // UI창 스택


    [SerializeField] private float dailyResultOpenInterval = 60f; // 정산창을 띄우기 위한 주기. 서버사용 없을 땐 약 5분 / 서버일 땐 하루단위 (86400초)로 설6


    //UI오픈에 반응하는 델리게이트 / 이벤트 선언하기 위한 필드
    public delegate void DailyResultWindowOn();
    public event DailyResultWindowOn OnDailyWindowOpen;

    private void Start()
    {
        InitUIList();   // 매니저 활성화시 전체 UI창 초기화 진행
        StartCoroutine(DailyResultWindowRoutine());
    }


    public void OpenWindow(UIBase uiPrefab)   //UI창을 열기 위한 메서드. 버튼에 스크립트로 이벤트리스너를 부여하는 방식으로 사용해야 할듯함. 
    {
        Debug.Log($"OpenWindow called in UIManager with prefab: {uiPrefab.name}");
        UIBase uiInstance = Instantiate(uiPrefab, transform).GetComponent<UIBase>();
        if (uiStack.Count > 0)    //처음 열리는 창이 아닐 때에는 
        {
            uiStack.Peek().gameObject.SetActive(false);   // 기존에 열려있던 창을 비활성화. Peek()이란 스택 맨 위를 확인하는 메서.
        }
        uiStack.Push(uiInstance);  //ui프리팹을 열어줌
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

    private void OpenDailyResultWindow()
    {
        if (currentDailyResultWindow != null)
        {
            Destroy(currentDailyResultWindow.gameObject);    //혹시나 이미 열려있는 상태일때 중첩되지 않도록 이전에 열린 창은 파기
        }
        currentDailyResultWindow = Instantiate(dailyResultWindow, transform);    //currentDailyResultWindow 생성
        currentDailyResultWindow.Initialize();  // 
        OnDailyWindowOpen?.Invoke();    // 일일정산창 열리면 이벤트를 발생시킴. 여기서는 
    }
}
