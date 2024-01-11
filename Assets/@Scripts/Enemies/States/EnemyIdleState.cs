using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private float _waitTime;
    private bool _isWander;
    private float _direction;
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        _isWander = false;
        _waitTime = 0f;
    }

    #region IState

    public override void OnStateEnter()
    {
        RandomGenerate();
    }

    public override void OnStateExit()
    {
        _animator.SetBool(AnimatorHash.Idle, false);
    }

    public override void OnStateStay()
    {
        if(_playerFinder.IsPlayerEnter)
        {
            _stateMachine.StateTransition(_stateMachine.StateDictionary[EnemyStateType.Chase]);
            return;
        }

        _waitTime -= Time.deltaTime;

        if(_waitTime <= 0)
        {
            RandomGenerate();
            return;
        }

        if(_isWander)
        {
            _rigidbody.velocity = Vector2.right * _enemyData.Speed * _direction;
        }
    }

    #endregion

    private void RandomGenerate()
    {
        _waitTime = Random.Range(0.5f, 1.2f);
        
        if(_isWander)
        {
            _isWander = false;
        }
        else
        {
            _isWander = Random.value > 0.7;
            _direction = Random.value > 0.5 ? -1 : 1;
        }

        if(_isWander)
        {
            _animator.SetBool(AnimatorHash.Walk, true);
            _animator.SetBool(AnimatorHash.Idle, false);
        }
        else
        {
            _animator.SetBool(AnimatorHash.Idle, true);
            _animator.SetBool(AnimatorHash.Walk, false);
        }
        
    }
}