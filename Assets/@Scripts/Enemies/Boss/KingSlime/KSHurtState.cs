using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSHurtState : EnemyHurtState
{
    public KSHurtState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void OnStateExit()
    {
        _animator.SetTrigger(AnimatorHash.HurtEnd);
    }

    public override void OnStateStay()
    {
        if(_isHurtEnded)
        {
            (_stateMachine as BossStateMachine).PatternTransition();
        }
    }


}
