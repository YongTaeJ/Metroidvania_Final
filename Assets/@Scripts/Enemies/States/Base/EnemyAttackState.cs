using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    #region Fields
    public bool _isAttackEnded;
    protected Transform _attackPivot;
    #endregion
    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        stateMachine.EventReceiver.OnAttackEnded += EndState;
        _attackPivot = stateMachine.transform.Find("Sprite/AttackPivot"); 
    }

    #region IState

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

    #endregion

    private void EndState()
    {
        _isAttackEnded = true;
    }

    protected void PlayAttackSound()
    {
        var attackSound = ResourceManager.Instance.GetAudioClip("EnemyAttackSound");
        SFX.Instance.PlayOneShot(attackSound, 0.5f);
    }
}
