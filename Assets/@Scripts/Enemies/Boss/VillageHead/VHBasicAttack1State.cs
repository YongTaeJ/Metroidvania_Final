using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VHBasicAttack1State : BossAttackState
{
    public VHBasicAttack1State(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        BossPatternType = BossPatternType.Melee;
    }

    public override void OnStateEnter()
    {
        _isAttackEnded = false;
        _animator.SetInteger(AnimatorHash.PatternNumber, 0);
    }

    public override void OnStateExit()
    {
        _animator.SetInteger(AnimatorHash.PatternNumber, -1);
    }

    public override void OnStateStay()
    {
        if(_isAttackEnded == true)
        {
            (_stateMachine as BossStateMachine).PatternTransition();
        }
    }

    protected override void OnAttack()
    {
        _attackCollider.enabled = true;
    }

    protected override void OnAttackEnd()
    {
        _attackCollider.enabled = false;
        _isAttackEnded = true;
    }
}
