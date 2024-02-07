using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveReturn : MonoBehaviour
{
    [SerializeField] private Transform[] movePos;
    [SerializeField] private float speed = 5f;
    private int posNum = 7;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = movePos[posNum].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(posNum != 0)
        {
            ReturnMove();
        }
    }

    public void ReturnMove()
    {
        transform.position = Vector2.MoveTowards
            (transform.position, movePos[posNum].transform.position, speed * Time.deltaTime);
        if (transform.position == movePos[posNum].transform.position)
            posNum--;
        if (posNum == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
