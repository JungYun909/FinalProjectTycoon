using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : TopDownPlayerController
{
    public static InputManager instance;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        instance = this;
    }

    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        Vector2 mouseDirection = _camera.ScreenToWorldPoint(newAim);
        //Vector2 newAim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CallLookEvent(mouseDirection);
        //CallLookEvent(newAim);
    }

    public void OnClick(InputValue value)
    {
        IsClicking = value.isPressed;
    }
}