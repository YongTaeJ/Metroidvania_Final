using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VHHurtState : EnemyHurtState
{
    private int _hurtCount;
    private int _maxHurtCount;
    public VHHurtState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        // TODO => EnemyHurtState에서 이벤트 걸리는게 반영 되는지 확인 필요
        _maxHurtCount = 4;
    }

    public override void OnStateEnter()
    {
        _hurtCount = 0;
        _isHurtEnded = false;
        _animator.SetBool(AnimatorHash.Hurt, true);
    }

    public override void OnStateStay()
    {
        if(_isHurtEnded)
        {
            (_stateMachine as BossStateMachine).PatternTransition();
        }
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
