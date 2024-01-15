using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackState : EnemyAttackState
{
    private Collider2D _attackCollider;
    public EnemyMeleeAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        _attackCollider = _attackPivot.GetComponent<BoxCollider2D>();
        _attackCollider.enabled = false;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();

        // TODO => Animation을 분화하여 콜라이더를 알맞은 타이밍에 껐다 키도록!!
        _attackCollider.enabled = true;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();

        _attackCollider.enabled = false;
    }

}
