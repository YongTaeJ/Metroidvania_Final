using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : IState
{
    #region Fields
    protected EnemyStateMachine _stateMachine;
    protected Rigidbody2D _rigidbody;
    protected EnemyData _enemyData;
    protected PlayerFinder _playerFinder;
    protected Animator _animator;
    #endregion

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _rigidbody = stateMachine.Rigidbody;
        _enemyData = stateMachine.EnemyData;
        _playerFinder = stateMachine.PlayerFinder;
        _animator = stateMachine.Animator;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public abstract void OnStateStay();
}
