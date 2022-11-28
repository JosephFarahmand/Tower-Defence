using UnityEngine;

public class CannonAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [NaughtyAttributes.AnimatorParam(nameof(animator), AnimatorControllerParameterType.Trigger),SerializeField] private string fireTrigger;

    public void FireAnimation()
    {
        animator.SetTrigger(fireTrigger);
    }
}