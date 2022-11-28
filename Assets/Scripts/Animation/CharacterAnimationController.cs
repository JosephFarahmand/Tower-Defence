using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField, AnimatorParam(nameof(animator), AnimatorControllerParameterType.Bool)] private string deadBool;

    public void DeadAnimation()
    {
        animator.SetBool(deadBool, true);
    }
}
