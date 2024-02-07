using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMove : MonoBehaviour
{
    [SerializeField] private Transform[] movePos;
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject GiftBtn;
    private int posNum = 0;

    private void Start()
    {
        transform.position = movePos[posNum].transform.position;
    }

    private void Update()
    {
        if(posNum != movePos.Length)
        {
            MovePath();
        }
    }

    private void MovePath()
    {
        transform.position = Vector2.MoveTowards
            (transform.position, movePos[posNum].transform.position, speed * Time.deltaTime);
        if (transform.position == movePos[posNum].transform.position)
            posNum++;
        if (posNum == movePos.Length)
        {
            //posNum = 0;
            GiftBtn.SetActive (true);
        }
    }

    
}
