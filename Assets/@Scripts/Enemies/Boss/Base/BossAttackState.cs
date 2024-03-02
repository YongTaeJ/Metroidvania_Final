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
    protected ObjectFlip _objectFlip;
    protected Transform _playerTransform;
    protected Transform _transform;
    protected float _direction;
    
    public BossAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        _attackPivot = stateMachine.transform.Find("Sprite/AttackPivot");
        _attackCollider = _attackPivot.GetComponent<Collider2D>();
        _eventReceiver = stateMachine.EventReceiver;
        
        _objectFlip = stateMachine.ObjectFlip;
        _playerTransform = stateMachine.PlayerFinder.CurrentTransform;
        _transform = stateMachine.transform;
    }

    public override void OnStateEnter()
    {
        _isAttackEnded = false;
        _eventReceiver.OnAttackStarted -= OnAttack;
        _eventReceiver.OnAttackEnded -= OnAttackEnd;
        _eventReceiver.OnAttackStarted += OnAttack;
        _eventReceiver.OnAttackEnded += OnAttackEnd;
        GetDirection();
        _objectFlip.Flip(_direction);
    }

    public override void OnStateExit()
    {
        _eventReceiver.OnAttackStarted -= OnAttack;
        _eventReceiver.OnAttackEnded -= OnAttackEnd;
    }

    protected abstract void OnAttack();

    protected abstract void OnAttackEnd();

    protected void GetDirection()
    {
        _direction = _playerTransform.position.x - _transform.position.x > 0 ? 1 : -1 ;
    }
}
