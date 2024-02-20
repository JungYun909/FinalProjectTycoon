using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : AnimationBase
{
    private static readonly int IsSpawning = Animator.StringToHash("IsSpawning");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsInteracting = Animator.StringToHash("IsInteracting");

    private Coroutine animCoroutine;
    
    private IngredientController controller;
    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<IngredientController>();
    }

    private void OnEnable()
    {
        Spawn();
        controller.OnInteractEvent += Interact;
    }

    private void Spawn()
    {
        animator.SetTrigger(IsSpawning);
        animCoroutine = StartCoroutine(startMove());
    }

    private void Move()
    {
        animator.SetBool(IsMoving, controller.movementController.curCoroutine != null);
    }

    private void Interact()
    {
        animator.SetTrigger(IsInteracting);
    }

    private IEnumerator startMove()
    {
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("IsSpawning") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
        
        Move();
        animCoroutine = null;
    }
}
