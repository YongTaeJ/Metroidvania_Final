using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtState : EnemyBaseState
{
    #region Fields
    protected bool _isHurtEnded;
    #endregion

    public EnemyHurtState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        stateMachine.EventReceiver.OnHurtEnded += EndState;
    }

    #region IState
    public override void OnStateEnter()
    {
        _isHurtEnded = false;
        _animator.SetBool(AnimatorHash.Hurt, true);
    }

    public override void OnStateExit()
    {
        _animator.SetBool(AnimatorHash.Hurt, false);
    }

    public override void OnStateStay()
    {
        if(_isHurtEnded)
        {
            _stateMachine.StateTransition(_stateMachine.StateDictionary[EnemyStateType.Idle]);
        }
    }
    #endregion

    protected virtual void EndState()
    {
        _isHurtEnded = true;
    }
}
