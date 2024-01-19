using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossNormalAttackState : BossBaseState
{
    protected bool _isAttackStarted;
    protected bool _isAttackEnded;
    public BossNormalAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        BossPatternType = BossPatternType.Melee;
    }

    public override void OnStateEnter()
    {
        _isAttackStarted = false;
        _isAttackEnded = false;
        // TODO => 애니메이션 설정
    }

    public override void OnStateExit()
    {
        // TODO => 애니메이션 설정
    }

    protected abstract void OnAttack();

    protected abstract void EndState();
}
