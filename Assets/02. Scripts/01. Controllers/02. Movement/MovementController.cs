using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementController : MonoBehaviour
{
    public void Move(GameObject destinationObject, float moveSpeed)
    {
        Vector2 moveDirection = (destinationObject.transform.position - gameObject.transform.position).normalized;
        Vector2 moveAmount = moveDirection * moveSpeed * Time.deltaTime;
        transform.position += new Vector3(moveAmount.x, moveAmount.y, 0f);
    }
}
