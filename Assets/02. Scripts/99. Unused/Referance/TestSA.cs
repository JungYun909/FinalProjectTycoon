using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSA : MonoBehaviour
{
    Sprite sprites;

    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.Load<Sprite>(ResourcePath.bread + "b1");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("d");
        collision.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = sprites;
    }
}
