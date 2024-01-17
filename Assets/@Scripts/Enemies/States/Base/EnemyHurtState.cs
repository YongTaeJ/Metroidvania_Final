using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtState : EnemyBaseState
{
    #region Fields
    private float _hurtTime;
    #endregion

    public EnemyHurtState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        _hurtTime = 0.2f;
    }

    #region IState
    public override void OnStateEnter()
    {
        _animator.SetBool(AnimatorHash.Hurt, true);
        _hurtTime = 0.2f;
    }

    public override void OnStateExit()
    {
        _animator.SetBool(AnimatorHash.Hurt, false);
    }

    public override void OnStateStay()
    {
        _hurtTime -= Time.deltaTime;
        if(_hurtTime <= 0)
        {
            _stateMachine.StateTransition(_stateMachine.StateDictionary[EnemyStateType.Idle]);
        }
    }
    #endregion
}
