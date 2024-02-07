using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class UIAnimationController : MonoBehaviour
{

    [SerializeField] GameObject UIObject;

    private void UIOpenAnim()
    {
        UIObject.SetActive(true);
    }
    private void UICloseAnim()
    {
        UIObject.SetActive(false);
    }



}
