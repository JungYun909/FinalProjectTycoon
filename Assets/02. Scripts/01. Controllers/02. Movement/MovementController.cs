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
    public bool isMove = false;
    public Coroutine curCoroutine;

    public float angle;
    private void Update()
    {
        
        if(speed <= 0 || !destinationObj)
            return;

        if (!isMove)
        {
            curCoroutine = StartCoroutine(Movement());
            isMove = true;
        }
    }

    public void Reset()
    {
        speed = 0f;
        destinationObj = null;
        isMove = false;
        
        if(curCoroutine != null)
            StopCoroutine(curCoroutine);
    }

    private IEnumerator Movement()
    {
        while (true)
        {
            Vector2 moveDirection = (destinationObj.transform.position - gameObject.transform.root.position).normalized;
            angle = Vector2.Angle(Vector2.right, moveDirection);
            Vector2 moveAmount = moveDirection * speed * Time.deltaTime;
            transform.root.position += new Vector3(moveAmount.x, moveAmount.y, 0f);
            yield return null;
        }
    }
}
