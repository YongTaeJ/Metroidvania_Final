using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : IState
{
    #region Fields
    protected EnemyStateMachine _stateMachine;
    protected Animator _animator;
    #endregion

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _animator = stateMachine.Animator;
    }

    #region IState
    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public abstract void OnStateStay();
    #endregion

}
