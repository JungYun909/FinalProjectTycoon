using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
    public UIManager uiManager; 
    public abstract void Initialize();
    public abstract void UpdateUI();

    private void OnEnable()
    {
        uiManager = FindObjectOfType<UIManager>();
        StatManager statManager = FindObjectOfType<StatManager>();   // TODO > FindObjectOfType무은 모두 수정 필요
        if(statManager != null)
        {
            statManager.onStatChanged += UpdateUI;
        }
    }

    private void OnDisable()
    {
        StatManager statManager = FindObjectOfType<StatManager>();
        if(statManager != null)
        {
            statManager.onStatChanged -= UpdateUI;
        }
    }
}
