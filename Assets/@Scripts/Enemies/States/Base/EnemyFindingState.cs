using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어를 찾지 못한 상태를 정의한 클래스입니다.
/// </summary>
public abstract class EnemyFindingState : EnemyBaseState
{
    protected float _stateTime;
    protected EnemyFindingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void OnStateStay()
    {
        if(_playerFinder.IsPlayerEnter)
        {
            _stateMachine.StateTransition(_stateMachine.StateDictionary[EnemyStateType.Chase]);
            return;
        }
    }

}
