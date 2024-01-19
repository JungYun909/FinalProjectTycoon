using UnityEngine;
using UnityEngine.InputSystem;

public class TempInput : MonoBehaviour
{
    public delegate void ClickAction(Vector2 position);
    public static event ClickAction OnClicked;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void OnClick(InputValue value)
    {
        if (value.isPressed)
        {
            Vector2 clickPosition = Mouse.current.position.ReadValue(); // 클릭했을 때의 좌표를 받아옴
            Vector2 worldPosition = _camera.ScreenToWorldPoint(clickPosition); // 스크린 좌표를 월드 좌표로 변환
            OnClicked?.Invoke(worldPosition); // 클릭 위치를 이벤트를 통해 전달
        }
    }
}
