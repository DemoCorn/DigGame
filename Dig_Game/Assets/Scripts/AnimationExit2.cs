using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationExit2 : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("EndAnimation", true);
        animator.SetBool("PlayAnimation", false);
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
