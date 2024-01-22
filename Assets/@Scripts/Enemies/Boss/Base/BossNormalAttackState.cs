using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAttackState : BossBaseState
{
    protected bool _isAttackEnded;
    protected Transform _attackPivot;
    protected Collider2D _attackCollider;
    public BossAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        BossPatternType = BossPatternType.Melee;
        _attackPivot = stateMachine.transform.Find("AttackPivot");
        _attackCollider = _attackPivot.GetComponent<Collider2D>();
        stateMachine.EventReceiver.OnAttackStarted += OnAttack;
        stateMachine.EventReceiver.OnAttackEnded += OnAttackEnd;
    }

    protected abstract void OnAttack();

    protected abstract void OnAttackEnd();
}
