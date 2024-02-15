using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScaleDown : TutorialBase
{
    [SerializeField] private RectTransform scaleTransform;
    private bool isCompleted = false;

    public override void Enter()
    {
        scaleTransform.gameObject.SetActive(true);

        StartCoroutine(TimeCheck());
    }

    private IEnumerator TimeCheck()
    {
        yield return new WaitForSeconds(1.5f);

        isCompleted = true;
    }

    public override void Execute(TutorialController tutorialController)
    {
        scaleTransform.sizeDelta -= 20 * Vector2.one * Time.deltaTime;

        if(isCompleted == true)
        {
            tutorialController.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        //scaleTransform.gameObject.SetActive (false);
    }
}
