using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseState : BossBaseState
{
    private float _chaseTime;
    private Transform _playerTransform;
    private Transform _enemyTransform;
    private Rigidbody2D _rigidbody;
    private ObjectFlip _objectFlip;
    private float _speed;
    public BossChaseState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        _enemyTransform = stateMachine.transform;
        _playerTransform = stateMachine.PlayerFinder.CurrentTransform;
        _speed = stateMachine.EnemyData.Speed;
        _rigidbody = stateMachine.Rigidbody;
        _objectFlip = stateMachine.ObjectFlip;
    }

    public override void OnStateEnter()
    {
        _chaseTime = Random.Range(0.5f, 0.7f);
        _animator.SetTrigger(AnimatorHash.Prepare);
        _animator.SetBool(AnimatorHash.Walk, true);
    }

    public override void OnStateExit()
    {
        _animator.SetBool(AnimatorHash.Walk, false);
    }

    public override void OnStateStay()
    {
        _chaseTime -= Time.deltaTime;
        if(_chaseTime <= 0)
        {
            (_stateMachine as BossStateMachine).PatternTransition();
        }
        else if(_playerTransform != null)
        {
            float direction =
            _playerTransform.position.x - _enemyTransform.position.x > 0 ?
            1f : -1f ;
            _objectFlip.Flip(direction);
            _rigidbody.velocity = new Vector2(direction * _speed , _rigidbody.velocity.y);
        }
    }
}
