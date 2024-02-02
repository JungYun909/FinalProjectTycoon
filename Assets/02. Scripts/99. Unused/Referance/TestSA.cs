using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSA : MonoBehaviour
{
    //Sprite sprites;
    public InstallationController installationController;
    public SpriteRenderer[] spriteRenderers; 
    // Start is called before the first frame update
    void Start()
    {
        //sprites = Resources.Load<Sprite>(ResourcePath.bread + "b1");
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.GetComponentsInChildren<SpriteRenderer>()[1].sprite == null)
    //    {
    //        collision.gameObject.GetComponentsInChildren<SpriteRenderer>()[1].sprite = sprites;
    //    }
    //}

    public void AddImage()
    {
        foreach (var sprite in spriteRenderers)
        {
            if (sprite.sprite != null)
            {
                sprite.sprite = installationController.ingredients.Dequeue().sprite;
            }
        }
    }
}
