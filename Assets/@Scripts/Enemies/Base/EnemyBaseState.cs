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

    public abstract void OnStateEnter();
    public abstract void OnStateExit();

    public virtual void OnStateStay()
    {
        // TODO => hit, dead 미리 검사하도록.
        // attack일때 hit은? 경우에 따라 관리.
    }
}
