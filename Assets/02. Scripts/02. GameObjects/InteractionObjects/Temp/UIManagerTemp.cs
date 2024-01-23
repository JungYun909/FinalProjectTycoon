using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerTemp : MonoBehaviour
{
    public static UIManagerTemp instance;
    
    public GameObject installationSetUI;
    public GameObject installUI;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        installationSetUI.gameObject.SetActive(false);
        installUI.gameObject.SetActive(false);
    }

    public void UIToggle(string uiName)
    {
        switch (uiName)
        {
            case "installationSetUI":
                installationSetUI.SetActive(!installationSetUI.activeSelf);
                return;
            case "installUI":
                installUI.SetActive(!installUI.activeSelf);
                return;
        }
    }

    public void UIImageChange(GameObject _gameObject ,Sprite newSprite)
    {
        SpriteRenderer spriteRenderer = _gameObject.GetComponent<SpriteRenderer>();
        Debug.Log(spriteRenderer.sprite);
        spriteRenderer.sprite = newSprite;
        Debug.Log(spriteRenderer.sprite);
    }
}