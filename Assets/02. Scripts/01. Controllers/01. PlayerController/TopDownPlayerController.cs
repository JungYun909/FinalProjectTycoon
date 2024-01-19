using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class TopDownPlayerController : MonoBehaviour
{
    public event Action OnClickEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action OffClickEvent;
    protected bool IsClicking { get; set; }

    protected virtual void Update()
    {
        HandleClick();
    }

    private void HandleClick()
    {
        if (IsClicking)
        {
            CallClickEvent();
        }
        else
        {
            CallOffClickEvent();
        }
    }

    public void CallClickEvent()
    {
        OnClickEvent?.Invoke();
    }

    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }

    public void CallOffClickEvent()
    {
        OffClickEvent?.Invoke();
    }
}