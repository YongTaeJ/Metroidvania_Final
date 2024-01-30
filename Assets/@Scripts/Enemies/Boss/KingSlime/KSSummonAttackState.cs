using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSSummonAttackState : BossAttackState
{
    public KSSummonAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        BossPatternType = BossPatternType.Special;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _animator.SetTrigger(AnimatorHash.Attack);
        _animator.SetInteger(AnimatorHash.PatternNumber, 2);
    }

    public override void OnStateExit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnStateStay()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnAttack()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnAttackEnd()
    {
        throw new System.NotImplementedException();
    }
}
