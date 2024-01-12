using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyIdleState : EnemyFindingState
{
    private float _direction;
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    #region IState

    public override void OnStateEnter()
    {
        _animator.SetBool(AnimatorHash.Idle, true);
        _stateTime = Random.Range(1f, 3f);
    }

    public override void OnStateExit()
    {
        _animator.SetBool(AnimatorHash.Idle, false);
    }

    public override void OnStateStay()
    {
        base.OnStateStay();

        _stateTime -= Time.deltaTime;

        if(_stateTime <= 0)
        {
            _stateMachine.StateTransition(_stateMachine.StateDictionary[EnemyStateType.Wander]);
            return;
        }
    }
    
    #endregion
}