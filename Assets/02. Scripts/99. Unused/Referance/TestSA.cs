using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSA : MonoBehaviour
{
    //Sprite sprites;
    public IngredientController controller;
    public SpriteRenderer[] spriteRenderers; 
    public void InitSetting()
    {
        foreach (var sprite in spriteRenderers)
        {
            sprite.sprite = null;
        }
    }
    

    public void AddImage(Sprite changeSprite)
    {
        foreach (var sprite in spriteRenderers)
        {
            if (sprite.sprite == null)
            {
                sprite.sprite = changeSprite;
                return;
            }
        }
    }
}
