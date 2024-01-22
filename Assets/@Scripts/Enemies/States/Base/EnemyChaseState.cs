using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    #region Fields
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private Vector2 _direction;
    private PlayerFinder _playerFinder;
    private ObjectFlip _objectFlip;
    private int _layerMask;
    private float _attackDistance;
    private float _speed;
    #endregion
    public EnemyChaseState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        _transform = stateMachine.transform;
        _layerMask = LayerMask.GetMask("Ground");
        _attackDistance = stateMachine.EnemyData.AttackDistance;
        _speed = stateMachine.EnemyData.Speed;
        _playerFinder = stateMachine.PlayerFinder;
        _rigidbody = stateMachine.Rigidbody;
        _objectFlip = stateMachine.ObjectFlip;
    }

    #region IState

    public override void OnStateEnter()
    {
        _animator.SetBool(AnimatorHash.Walk, true);
    }

    public override void OnStateExit()
    {
        _animator.SetBool(AnimatorHash.Walk, false);
    }

    public override void OnStateStay()
    {
        if(!_playerFinder.IsPlayerEnter)
        {
            _stateMachine.StateTransition(_stateMachine.StateDictionary[EnemyStateType.Idle]);
            return;
        }
        
        float value = _playerFinder.CurrentTransform.position.x - _transform.position.x;
        _objectFlip.Flip(value);
        
        if(Mathf.Abs(value) < _attackDistance)
        {
            if(_stateMachine.StateDictionary.TryGetValue(EnemyStateType.Attack, out var attackState))
                _stateMachine.StateTransition(attackState);
            return;
        }

        _direction = value > 0 ? Vector2.right : Vector2.left;
        FallCheck();

        _rigidbody.velocity = new Vector2(_direction.x * _speed, _rigidbody.velocity.y);
    }

    #endregion

    private void FallCheck()
    {
        Vector2 currentPosition = _transform.position;

        bool isLand = Physics2D.Raycast(currentPosition + _direction, Vector2.down, 1f, _layerMask);

        if(!isLand) _direction = Vector2.zero;
    }
}
