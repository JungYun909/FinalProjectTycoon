using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
    public abstract void Initialize();
    public abstract void Show();
    public abstract void Hide();
    public abstract void UpdateUI();
}
