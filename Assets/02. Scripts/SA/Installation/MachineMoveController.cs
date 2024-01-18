using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
public class MachineMoveController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isButtonPressed = false;
    public Tilemap tilemap;
    private void Awake()
    {
        InputManager.instance.OnLookEvent += MousePositionUpdate;
        
    }

    private void MousePositionUpdate(Vector2 direction)
    {
        if (isButtonPressed)
        {

            Debug.Log(isButtonPressed);
            transform.position = tilemap.WorldToCell(direction);
            transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y + 0.3f);
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
