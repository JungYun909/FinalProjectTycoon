using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBtn : InteractionData
{
    public override void InitSetting()
    {
        _interactionStat.name = "Delete Button";
        _interactionStat.type = InteractionType.UI;
    }

    public override void OnInteract()
    {
        Destroy(_interactionStat.curGameObject);
        gameObject.transform.parent.gameObject.SetActive(false);
        //인벤토리에 넣는다
    }
}
