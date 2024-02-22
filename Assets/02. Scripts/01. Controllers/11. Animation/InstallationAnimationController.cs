using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public enum InstallationAnimType
{
    Spawn,
    Interact
}
public class InstallationAnimationController : AnimationBase
{
    private static readonly int Spawn = Animator.StringToHash("Spawn");

    private InstallationController _controller;
    private AnimatorOverrideController _animatorController;
    private List<AnimatorState> _animatorStates;
    
    protected override void Awake()
    {
        base.Awake();
        _controller = GetComponent<InstallationController>();
        _animatorController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = _animatorController;
    }

    public void AddAnimation(AnimationClip anim, InstallationAnimType type)
    {
        _animatorController[type.ToString()] = anim;
        // _animatorController.layers[0].stateMachine.states[(int)type + 1].state.motion = anim;
    }

    public void StartSpawnAnim()
    {
        animator.SetTrigger(Spawn);
    }

    public void OnEndAnim()
    {
        animator.speed = 1;
    }
}
