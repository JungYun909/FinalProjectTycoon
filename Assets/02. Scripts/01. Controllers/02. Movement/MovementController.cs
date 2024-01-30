using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementController : MonoBehaviour
{
    public float speed;
    public GameObject destinationObj;
    public bool isMove;

    private void Update()
    {
        
        if(speed <= 0 && !destinationObj)
            return;

        StartCoroutine(Movement());
    }

    private IEnumerator Movement()
    {
        while (Vector2.Distance(destinationObj.transform.position, gameObject.transform.root.position) > 0.1f)
        {
            Vector2 moveDirection = (destinationObj.transform.position - gameObject.transform.root.position).normalized;
            Vector2 moveAmount = moveDirection * speed * Time.deltaTime;
            transform.root.position += new Vector3(moveAmount.x, moveAmount.y, 0f);
            yield return null;
        }
    }
}
