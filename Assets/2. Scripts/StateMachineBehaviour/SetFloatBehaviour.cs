using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFloatBehaviour : StateMachineBehaviour
{
    public string floatName;
    public bool updateOnStateEnter, updateOnStateExit;
    public bool updateOnStateMachineEnter, updateOnStateMachineExit;
    public float valueOnEnter, valueOnExit;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(updateOnStateEnter)
        {
            animator.SetFloat("AttackCoolDown", valueOnEnter);
        }
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(updateOnStateExit)
        {
            animator.SetFloat("AttackCoolDown", valueOnExit);
        }
    }

    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if(updateOnStateMachineEnter)
        {
            animator.SetFloat("AttackCoolDown", valueOnEnter);
        }
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if(updateOnStateMachineExit)
        {
            animator.SetFloat("AttackCoolDown", valueOnExit);
        }
    }
}
