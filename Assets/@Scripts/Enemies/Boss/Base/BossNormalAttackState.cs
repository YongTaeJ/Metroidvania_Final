using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public abstract class BossAttackState : BossBaseState
{
    protected bool _isAttackEnded;
    protected Transform _attackPivot;
    protected Collider2D _attackCollider;
    protected EnemyAnimationEventReceiver _eventReceiver;
    public BossAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        BossPatternType = BossPatternType.Melee;
        _attackPivot = stateMachine.transform.Find("Sprite/AttackPivot");
        _attackCollider = _attackPivot.GetComponent<Collider2D>();
        _eventReceiver = stateMachine.EventReceiver;
    }

    public override void OnStateEnter()
    {
        _eventReceiver.OnAttackStarted -= OnAttack;
        _eventReceiver.OnAttackEnded -= OnAttackEnd;
        _eventReceiver.OnAttackStarted += OnAttack;
        _eventReceiver.OnAttackEnded += OnAttackEnd;
    }

    public override void OnStateExit()
    {
        _eventReceiver.OnAttackStarted -= OnAttack;
        _eventReceiver.OnAttackEnded -= OnAttackEnd;
    }

    protected abstract void OnAttack();

    protected abstract void OnAttackEnd();
}
