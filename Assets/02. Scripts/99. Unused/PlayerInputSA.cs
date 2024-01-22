using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputSA : MonoBehaviour
{
    // private Vector2 curMousePosition;
    // public GameObject curObj;
    // private void Start()
    // {
    //     InputManager.instance.OnLookEvent += MousePositionUpdate2;
    //     InputManager.instance.OnClickEvent += IsClicking;
    // }
    // public void MousePositionUpdate2(Vector2 mousePosition)
    // {
    //     curMousePosition = mousePosition;
    // }
    //
    // public void IsClicking()
    // {
    //
    //     // 레이를 발생한다(마우스의 위치, vectior제로, 0)
    //     // 레이힛 if 힛 콜라이더가 널이면 리턴, 아니면 ㄱ, 힛 콜라이더의 레이어가 설치물일경우에 발생
    //     // if유아이캔버스가 안켜져있을경우에 켜준다
    //     // curSpriteRenderer가 플레이어의 스프라이트랜더러 > 레이케스트가 감지한 콜라이더의 게임오브젝트의 스프라이트랜더러를 가져옴
    //     // 플레이어 게임오브젝트> 레이케스트가 감지한 게임오브젝트의 트랜스폼닷포지션
    //     
    //     RaycastHit2D hit = Physics2D.Raycast(curMousePosition, Vector2.zero, 0);
    //     
    //     if (hit.collider == null)
    //     {
    //         return;
    //     }
    //     if (!UIManagerTemp.instance.canvas.gameObject.activeSelf)
    //     {
    //         UIManagerTemp.instance.canvas.gameObject.SetActive(true);
    //     }
    //     switch (hit.collider.gameObject.layer)
    //     {
    //         case 10:
    //             if (curObj != null)
    //             {
    //                 // curObj의 트랜스폼 포지션을 캔버스의 트랜스폼 포지션으로 설정
    //                 curObj.transform.position = UIManagerTemp.instance.canvas.transform.position;
    //                 curObj.SetActive(true);
    //             }
    //             curObj = hit.collider.gameObject;
    //             SpriteRenderer curSpriteRenderer = hit.collider.GetComponentInChildren<SpriteRenderer>();
    //             UIManagerTemp.instance.canvas.gameObject.transform.position = hit.collider.gameObject.transform.position;
    //             UIManagerTemp.instance.UIImageChange(curSpriteRenderer.sprite);
    //             curObj.SetActive(false);
    //             break;
    //     }
    // }
}
