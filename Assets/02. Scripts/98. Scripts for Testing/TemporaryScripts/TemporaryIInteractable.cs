using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryIInteractable : MonoBehaviour, IInteractable
{

    [SerializeField] UIBase uiToOpen;
    public bool Continuous()
    {
        return false;
    }

    public void OnInteract()
    {

        Debug.Log("Interacted with this object"); // 상호작용 로깅
	    UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.OpenWindow(uiToOpen);
            Debug.Log("UIManager found, calling OpenWindow");
        }
        else
        {
            Debug.Log("UIManager not found");
        }
    }
}
