using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialBase : MonoBehaviour
{
    //튜토리얼을 시작할 때 1회 호출
    public abstract void Enter();

    //튜토리얼을 진행하는동안 매 프레임 호출
    public abstract void Execute(TutorialController tutorialController);

    //튜토리얼을 종료할 때 1회 호출
    public abstract void Exit();
}
