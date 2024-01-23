using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementController : MonoBehaviour
{
    private GameObject curDestinationObject;
    private float curMoveSpeed;
    public void Move(GameObject destinationObject)
    {
        curDestinationObject = destinationObject;
        curMoveSpeed = gameObject.GetComponent<IngredientData>().stat.moveSpeed;
        StartCoroutine("Movement");
    }

    private IEnumerator Movement()
    {
        while (Vector2.Distance(curDestinationObject.transform.position, gameObject.transform.position) > 0.1f)
        {
            Vector2 moveDirection = (curDestinationObject.transform.position - gameObject.transform.position).normalized;
            Vector2 moveAmount = moveDirection * curMoveSpeed * Time.deltaTime;
            transform.position += new Vector3(moveAmount.x, moveAmount.y, 0f);

            yield return null;
        }
    }
}
