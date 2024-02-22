using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VHLeapAttackState : BossAttackState
{
    private int _startCount;
    private int _endCount; 

    private Rigidbody2D _rigidbody;
    private Collider2D _throwCollider;
    private Collider2D _bodyTriggerCollider;
    private Collider2D _bodyCollisionCollider;
    private float _throwDistance;
    private float _jumpTime;

    public VHLeapAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        BossPatternType = BossPatternType.Ranged;

        _throwCollider = stateMachine.transform.Find("Sprite/AttackPivot/ThrowAttack").GetComponent<Collider2D>();
        _bodyTriggerCollider = stateMachine.transform.Find("Sprite/AttackPivot/Body").GetComponent<CapsuleCollider2D>();
        _bodyCollisionCollider = stateMachine.transform.GetComponent<Collider2D>();
        _rigidbody = stateMachine.Rigidbody;

        _jumpTime = 1f;
        _throwDistance = 4.5f;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _animator.SetTrigger(AnimatorHash.Attack);
        _animator.SetInteger(AnimatorHash.PatternNumber, 3);
        _startCount = 0;
        _endCount = 0;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        _animator.SetInteger(AnimatorHash.PatternNumber, -1);
    }

    public override void OnStateStay()
    {
    }

    protected override void OnAttack()
    {
        _startCount++;

        if(_startCount == 1)
        {
            SFX.Instance.PlayOneShot("EnemyAttackSound", 0.7f);
            _direction = _playerTransform.position.x - _transform.position.x > 0 ? 1 : -1 ;
            _objectFlip.Flip(_direction);
            _transform.DOMoveX(_playerTransform.position.x - _throwDistance * _direction, _jumpTime);
            _bodyTriggerCollider.enabled = false;
            _bodyCollisionCollider.enabled = false;
            _rigidbody.isKinematic = true;
        }
        else if(_startCount == 2)
        {
            SFX.Instance.PlayOneShot("EnemyAttackSound");
            _throwCollider.enabled = true;
        }
        else if(_startCount == 3)
        {
            SFX.Instance.PlayOneShot("VillageHeadLeapAttackSound", 0.5f);
            _bodyTriggerCollider.enabled = true;
            _bodyCollisionCollider.enabled = true;
            _rigidbody.isKinematic = false;
            _transform.position += Vector3.right * _direction * _throwDistance * 1.7f;
        }
    }

    protected override void OnAttackEnd()
    {
        _endCount++;

        if(_endCount == 1)
        {
            _throwCollider.enabled = false;
        }
        else if(_endCount == 2)
        {
            (_stateMachine as BossStateMachine).PatternTransition();
        }
    }
}
