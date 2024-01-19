using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerTemp : MonoBehaviour
{
    public static UIManagerTemp instance;
    public Canvas canvas;
    public GameObject _gameObject;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        canvas.gameObject.SetActive(false);
    }

    public void OnUI()
    {
        canvas.gameObject.SetActive(true);
    }

    public void OffUI()
    {
        canvas.gameObject.SetActive(false);
    }

    public void UIImageChange(Sprite newSprite)
    {
        SpriteRenderer spriteRenderer = _gameObject.GetComponent<SpriteRenderer>();
        Debug.Log(spriteRenderer.sprite);
        spriteRenderer.sprite = newSprite;
        Debug.Log(spriteRenderer.sprite);
    }
}
