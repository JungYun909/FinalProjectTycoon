using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public void OpenUI(GameObject curGameObject)
    {
        curGameObject.SetActive(true);
    }
    
    public void CloseUI(GameObject curGameObject)
    {
        curGameObject.SetActive(false);
    }
}
