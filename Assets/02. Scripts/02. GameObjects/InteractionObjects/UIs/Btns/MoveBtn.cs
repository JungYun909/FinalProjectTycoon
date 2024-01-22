using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveBtn : InteractionData
{
    public override void InitSetting()
    {
        _interactionStat.name = "Move Button";
        _interactionStat.type = InteractionType.UI;
        _interactionStat.isClick = true;
    }

    public override void OnInteract()
    {
        Vector2 mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.parent.position = new Vector2(mouseDirection.x, mouseDirection.y + (gameObject.transform.parent.position.y - gameObject.transform.position.y));
        _interactionStat.curGameObject.transform.position = gameObject.transform.parent.position;
    }
}
