using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VHHurtState : EnemyHurtState
{
    private int _hurtCount;
    private int _maxHurtCount;
    public VHHurtState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        _maxHurtCount = 4;
    }

    public override void OnStateEnter()
    {
        _hurtCount = 0;
        _isHurtEnded = false;
        _animator.SetTrigger(AnimatorHash.Hurt);
    }

    public override void OnStateStay()
    {
        if(_isHurtEnded)
        {
            (_stateMachine as BossStateMachine).PatternTransition();
        }
    }

    public override void OnStateExit()
    {
        _animator.SetTrigger(AnimatorHash.HurtEnd);
    }

    protected override void EndState()
    {
        _hurtCount++;

        if(_hurtCount >= _maxHurtCount)
        {
            _isHurtEnded = true;
        }
    }
}
