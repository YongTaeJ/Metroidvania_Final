using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public bool _isAttackEnded;
    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        stateMachine.EventReceiver.OnAttackEnded -= EndState;
        stateMachine.EventReceiver.OnAttackEnded += EndState; // TODO => 따로 OnStateEnter <-> Exit에서 관리해야하는지
    }

    public override void OnStateEnter()
    {
        _isAttackEnded = false;
        _animator.SetTrigger(AnimatorHash.Attack);
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateStay()
    {
        if(_isAttackEnded)
        {
            _stateMachine.StateTransition(_stateMachine.StateDictionary[EnemyStateType.Idle]);
        }
    }

    public void EndState()
    {
        _isAttackEnded = true;
    }
}
