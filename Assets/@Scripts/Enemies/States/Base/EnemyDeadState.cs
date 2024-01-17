using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    #region Fields
    private float _deadTime;
    private Transform _spriteTransform;
    #endregion
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        _deadTime = 0.2f;
        _spriteTransform = stateMachine.transform.Find("Sprite");
    }
    #region IState
    public override void OnStateEnter()
    {
        _animator.SetTrigger(AnimatorHash.Dead);
        GenerateDeadEffect();
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateStay()
    {
        _deadTime -= Time.deltaTime;
        if(_deadTime <= 0)
        {
            DropManager.Instance.DropItem(_stateMachine.EnemyData.DropTableIndex, _spriteTransform.position);
            GameObject.Destroy(_stateMachine.gameObject);
        }
    }
    #endregion
    
    private void GenerateDeadEffect()
    {
        GameObject effect = Resources.Load<GameObject>("Enemies/Effects/EnemyDeadEffect");
        Transform transform = GameObject.Instantiate(effect).GetComponent<Transform>();
        transform.localPosition = _spriteTransform.position;
    }
}
