using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public struct MoveData
{
    public float speed; //속도
}
public abstract class MovementObject : MonoBehaviour
{
    public MoveData moveData;
    public abstract void InitSetting();

    public virtual void Spawn(GameObject gameObject, GameObject startGameObject)
    {
        Instantiate(gameObject, startGameObject.transform.position, Quaternion.identity);
    }

    public virtual void DeSpawn(GameObject gameObject, GameObject endGameObject)
    {
        if (Vector2.Distance(gameObject.transform.position, endGameObject.transform.position) < 0.1f)
        {
            //TODO 해당 사물에 이 게임 오브젝트의 값을 참조하여 추가할거 추가
            Destroy(gameObject);
        }
    }
}