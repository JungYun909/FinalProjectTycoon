using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class MachineMoveController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isButtonPressed = false;

    private void Awake()
    {
        InputManager.instance.OnLookEvent += MousePositionUpdate;
    }

    private void MousePositionUpdate(Vector2 direction)
    {
        if (isButtonPressed)
        {
            Debug.Log(isButtonPressed);
            transform.position = direction;
            Transform prtransform = transform.root;
            prtransform.position = direction;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonPressed = true;
        Debug.Log(isButtonPressed);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonPressed = false;
        Debug.Log(isButtonPressed);
    }
}
