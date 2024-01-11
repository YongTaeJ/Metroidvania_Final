using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    private Transform _myPosition;
    public EnemyChaseState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        _myPosition = _rigidbody.transform;
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
            _stateMachine.StateTransition(_stateMachine.StateDictionary[EnemyStateType.Attack]);
            return;
        }

        float value = _playerFinder.CurrentPosition.position.x - _myPosition.position.x;
        float direction = value > 0 ? 1 : -1;
        _rigidbody.velocity = Vector2.right * direction * _enemyData.Speed;
    }

    #endregion
}
