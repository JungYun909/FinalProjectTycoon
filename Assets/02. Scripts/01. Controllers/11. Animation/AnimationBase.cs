using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBase : MonoBehaviour
{
    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
}