using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Kneader : InteractionData
{
    public Sprite curIcon;
    public GameObject InstallationUI;
    
    public override void InitSetting()
    {
        _interactionStat.name = "Kneader";
        _interactionStat.description = "This is a machine that produces dough";
        _interactionStat.type = InteractionType.Installation;
        _interactionStat.icon = curIcon;
        _interactionStat.canCell = true;
        _interactionStat.price = 100000;
        _interactionStat.canMove = false;
        _interactionStat.speed = 0f;
    }

    public override void OnInteract()
    {
        InteractionObject[] btns = InstallationUI.GetComponentsInChildren<InteractionObject>();
        
        InstallationUI.SetActive(true);
        InstallationUI.transform.position = gameObject.transform.position;

        foreach (InteractionObject btn in btns)
        {
            btn._interactionData._interactionStat.curGameObject = gameObject;
        }
    }
}
