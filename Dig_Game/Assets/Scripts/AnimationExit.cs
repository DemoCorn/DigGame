using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationExit : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("WeaponAttackDone", true);
        animator.SetBool("PlayAttack", false);
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
