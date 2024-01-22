using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : TopDownPlayerController
{
    // public static InputManager instance;
    // private Camera _camera;
    // private Vector2 curMouseDirection;
    //
    // private void Awake()
    // {
    //     _camera = Camera.main;
    //     instance = this;
    // }
    //
    // public void OnLook(InputValue value)
    // {
    //     curMouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    // }
    //
    // public void OnClick(InputValue value)
    // {
    //     IsClicking = value.isPressed;
    //     RaycastHit2D ray = Physics2D.Raycast(curMouseDirection, Vector2.zero, 0f);
    //     
    //     if(!ray.collider)
    //         return;
    //
    //     InteractionObject curUIObject = ray.collider.gameObject.GetComponent<InteractionObject>();
    //     curUIObject._interactionData.InitSetting();
    //     curUIObject._interactionData.OnInteract();
    //}
}