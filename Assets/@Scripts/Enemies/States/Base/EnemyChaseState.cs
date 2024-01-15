using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    private Transform _myTransform;
    private Vector2 _direction;
    private int _layerMask;
    public EnemyChaseState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        _myTransform = _rigidbody.transform;
        _layerMask = LayerMask.GetMask("Water");
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
        
        // TODO => AttackDistance 필요
        float value = _playerFinder.CurrentTransform.position.x - _myTransform.position.x;
        if(Mathf.Abs(value) < 1.5f)
        {
            if(_stateMachine.StateDictionary.TryGetValue(EnemyStateType.Attack, out var enemyBaseState))
            _stateMachine.StateTransition(enemyBaseState);
            return;
        }

        _direction = value > 0 ? Vector2.right : Vector2.left;
        FallCheck();

        _rigidbody.velocity = _direction * _enemyData.Speed;
    }

    #endregion

    private void FallCheck()
    {
        Vector2 currentPosition = _myTransform.position;

        bool isLand = Physics2D.Raycast(currentPosition + _direction, Vector2.down, 1f, _layerMask);

        if(!isLand) _direction = Vector2.zero;
    }
}
