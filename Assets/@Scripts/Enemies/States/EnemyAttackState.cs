using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private AnimatorStateInfo _stateInfo;
    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        
    }

    public override void OnStateEnter()
    {
        _animator.SetBool(AnimatorHash.Attack, true);
        // TODO. 공격용 콜라이더 켜기
    }

    public override void OnStateExit()
    {
        _animator.SetBool(AnimatorHash.Attack, false);
    }

    public override void OnStateStay()
    {
        _stateInfo = _animator.GetNextAnimatorStateInfo(0);
        if (_stateInfo.IsName("Attack") && _stateInfo.normalizedTime >= 1)
        {
            Debug.Log("IsTransition");
            _stateMachine.StateTransition(_stateMachine.StateDictionary[EnemyStateType.Idle]);
        }
    }
}
