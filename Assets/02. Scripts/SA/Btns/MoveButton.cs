using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private bool isButtonPressed = false;
   
  

    private void Start()
    {
        InputManager.instance.OnLookEvent += Move;
    }

    private void Move(Vector2 vector)
    {
        Debug.Log(isButtonPressed);
        if (isButtonPressed)
        {
            transform.position = vector;
            Transform prtransform = transform.root;
            prtransform.position = vector;
        }
    }

    

    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonPressed = true;
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonPressed = false;
    }
}
