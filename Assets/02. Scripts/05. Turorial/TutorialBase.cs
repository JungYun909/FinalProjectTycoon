using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public abstract class TutorialBase : MonoBehaviour
{
    public string tutoID;
    public bool completed = false;

    //튜토리얼을 시작할 때 1회 호출
    public virtual void Enter()
    {
        GameManager.instance.interactionManager.targetID = tutoID;
        GameManager.instance.interactionManager.onTuto += Complete;
    }

    //튜토리얼을 진행하는동안 매 프레임 호출
    public abstract void Execute(TutorialController tutorialController);

    //튜토리얼을 종료할 때 1회 호출
    public virtual void Exit()
    {
        GameManager.instance.interactionManager.onTuto -= Complete;
    }

    public void Complete()
    {
        completed = true;
    }
}
