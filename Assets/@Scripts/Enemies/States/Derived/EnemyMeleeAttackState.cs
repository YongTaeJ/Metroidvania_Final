using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackState : EnemyAttackState
{
    #region Fields
    private Collider2D _attackCollider;
    #endregion

    public EnemyMeleeAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        stateMachine.EventReceiver.OnAttackStarted += ActiveAttackCollider;
        _attackCollider = _attackPivot.GetComponent<BoxCollider2D>();
        _attackCollider.enabled = false;
    }

    #region IState
    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();

        _attackCollider.enabled = false;
    }

    private void ActiveAttackCollider()
    {
        _attackCollider.enabled = true;
    }
    #endregion

}
