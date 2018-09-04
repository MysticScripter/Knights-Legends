using UnityEngine;

public class DeadOnce : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isDeadOnce", true);
    }
}
