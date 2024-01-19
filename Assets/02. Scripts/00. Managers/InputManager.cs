using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour  // TODO : FSM 강의 이후 다시 확인할 필요 있음. FSM 사용하고 이 스크립트를 사용할, 아니면 다른 SM 사용할지 
{
    public delegate void ClickAction(Vector2 position);  // 클릭 위치를 받는 클릭 액션 대리자를 정의합니다.
    public static event ClickAction OnClicked; // 클릭 이벤트를 다른 스크립트에서 구독할 수 있게 합니다.

    public PlayerInputSystem playerInputSystem { get; private set; } // 입력 시스템 인스턴스를 저장합니다.
    public PlayerInputSystem.PlayerActions playerActions { get; private set; } // 플레이어 액션을 저장합니다.

    private Camera _camera; // 사용할 카메라의 참조

    private void Awake()
    {
        _camera = Camera.main; // 메인 카메라를 찾아 할당
        playerInputSystem = new PlayerInputSystem(); // 인풋시슷템 인스턴스를 생성
        playerActions = playerInputSystem.Player; // 플레이어 액션을 할당
        playerActions.Click.performed += context => OnClick(context); // 클릭 액션에 대한 콜백을 설정
    }

    private void OnEnable()
    {
        playerInputSystem.Enable(); // 입력 시스템을 활성화
    }

    private void OnDisable()
    {
        playerInputSystem.Disable(); // 입력 시스템을 비활성화
    }

    //TODO 여기까지 생명주기와 관련된 함수들은 추후 초기화 & 활성화 메서드를 새로 짜고 GameManager가 관리하도록 만들어야.
    
    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>(); // 마우스 또는 컨트롤러 입력으로부터 aim 값을 가져옴. 현재는 사용 X?
        Vector2 mouseDirection = _camera.ScreenToWorldPoint(newAim); // 스크린 좌표를 월드 좌표로 변환

        // 여기에서 마우스 방향을 처리하는 로직을 추가할 수 있습니다.
        // 예: CallLookEvent(mouseDirection);
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        Debug.Log("OnClick in InputManager called");
        if (context.performed)
        {
            // 클릭 이벤트가 발생하면 마우스 위치를 읽어옵니다.
            Vector2 clickPosition = Mouse.current.position.ReadValue(); // 클릭 시 마우스의 스크린 좌표를 가져옴
            Vector2 worldPosition = _camera.ScreenToWorldPoint(clickPosition); // 스크린 좌표를 월드 좌표로 변환 > 클릭시 위치에 레이캐스트 발사하기 위함 (-10이라는 카메라와 오브젝트간 좌표차)
            OnClicked?.Invoke(worldPosition); // 클릭 위치를 이벤트를 통해 전달.
            Debug.Log($"Clicked at: {worldPosition}"); // 클릭 위치를 로그로 찍어.
        }
    }   //InputAction에서 뻗어나가는게 delegate 필요가 없 ?
        //온클릭 필요하다면 달아주는게 아니라  
}
