using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementController : MonoBehaviour
{
    public GameObject curDestinationObject;
    public float moveSpeed;

    public void Move(GameObject destinationObject, IngredientData data)
    {
        curDestinationObject = destinationObject;
        moveSpeed = data.moveSpeed;
        StartCoroutine("Movement");
    }

    private IEnumerator Movement()
    {
        while (Vector2.Distance(curDestinationObject.transform.position, gameObject.transform.position) > 0.1f)
        {
            Vector2 moveDirection = (curDestinationObject.transform.position - gameObject.transform.root.position).normalized;
            Vector2 moveAmount = moveDirection * moveSpeed * Time.deltaTime;
            transform.root.position += new Vector3(moveAmount.x, moveAmount.y, 0f);

            yield return null;
        }
    }
}
