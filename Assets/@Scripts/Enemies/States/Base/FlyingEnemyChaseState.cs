using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyChaseState : EnemyBaseState
{
    private PlayerFinder _playerFinder;
    private Transform _playerTransform;
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private float _currentTime;
    private float _attackTime;
    private float _speed;
    public FlyingEnemyChaseState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        _playerFinder = stateMachine.PlayerFinder;
        _transform = _stateMachine.transform;
        _rigidbody = _stateMachine.Rigidbody;
        _attackTime = 1.5f;
        _speed = stateMachine.EnemyData.Speed;
    }

    public override void OnStateEnter()
    {
        _playerTransform = _playerFinder.CurrentTransform;
        _currentTime = 0f;
        _animator.SetBool(AnimatorHash.Walk, true);
    }

    public override void OnStateExit()
    {
        _rigidbody.velocity = Vector2.zero;
        _animator.SetBool(AnimatorHash.Walk, false);
    }

    public override void OnStateStay()
    {
        _currentTime += Time.deltaTime;
        if(_currentTime >= _attackTime && _stateMachine.StateDictionary.ContainsKey(EnemyStateType.Attack))
        {
            _stateMachine.StateTransition(_stateMachine.StateDictionary[EnemyStateType.Attack]);
            return;
        }

        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        Vector2 direction = (_playerTransform.position - _transform.position).normalized;
        _rigidbody.velocity = direction * _speed;
    }
}
